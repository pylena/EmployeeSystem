using Dapper;
using EmployeeSystem.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem
{
    public class EmployeeRepository
    {
        private readonly AppDbContext _db;
        private readonly string _connectionString;
        private DbContextOptions<AppDbContext> options;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
            _connectionString = "Server=.;Database=EDB;Trusted_Connection=True;TrustServerCertificate=True"; //  connection string
        }

        public EmployeeRepository(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }

        //Tas2: Write a LINQ query using Entity Framework Core to find all employees who have worked on more than 3 projects in the last 6 months.
        public void EmployeesWithMoreThan3Projects()
        {

            var sixMonthsAgo = DateTime.Today.AddMonths(-6); //get last six months

            var employees = _db.Employees
                .Where(e => e.EmployeeProjects
                    .Count(ep => ep.Project.Deadline >= sixMonthsAgo) > 3)
                .ToList();

            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee with more than 3 projects: {employee.Name}");
            }

        }

        //Task3: retrieves all employees along with theier assigned projects, fetching only essential columns(EmployeeName, ProjectName ProjectDeadline).
        public void fetchEmployees()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = @"
                 Select e.Name, p.Title, p.Deadline
                  From employees e
                 JOIN EmployeeProjects ep ON e.Id = ep.EmployeeID
                JOIN Projects p ON ep.ProjectID = p.Id";

                var results = db.Query(query);

                foreach (var result in results)
                {
                    Console.WriteLine($"Employee: {result.Name}, Project: {result.Title}, Deadline: {result.Deadline}");
                }
            }


        }

        public void findPreformance()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT 
                    e.Id ,
                    e.Name ,
                    e.Salary,
                    e.Rating,
                    CASE 
                        WHEN e.PerformanceRating >= 9 THEN e.Salary * 0.2
                        WHEN e.PerformanceRating >= 7 THEN e.Salary * 0.1
                        WHEN e.PerformanceRating >= 5 THEN e.Salary * 0.05
                        ELSE 0  
                    END AS Bonus
                FROM Employees e;";
                var employees = db.Query(query).ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"Employee: {employee.Name}, Salary: {employee.Salary}, " +
                                      $"Performance: {employee.Rating}, Bonus: {employee.Bonus}");
                }




            }
        }

        //Using Dapper
        public void fetchreports()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
                      SELECT 
                    d.Name AS DepartmentName,
                    p.Title AS ProjectTitle,
                    SUM(p.Budget) AS TotalBudget,
                    SUM(e.Salary) AS TotalSalaries
                FROM Departments d
                JOIN Employees e ON d.DepartmentId = e.DepartmentId
                JOIN EmployeeProjects ep ON e.Id = ep.EmployeeID
                JOIN Projects p ON ep.ProjectID = p.ID
                GROUP BY d.Name, p.Title;";

                var results = db.Query(query).ToList();
                foreach (var result in results)
                {
                    Console.WriteLine($"Department: {result.DepartmentName}, " +
                                  $"Project: {result.ProjectTitle}, " +
                                  $"Total Budget: {result.TotalBudget}, " +
                                  $"Total Salaries: {result.TotalSalaries}");
                }



            }

        }

        public void repoertWithEF()
        {
            var repoert = _db.Employees
                .Join(_db.EmployeeProjects, e => e.Id, ep => ep.EmployeeID, (e, ep) => new { e, ep })
                .Join(_db.Projects, ee => ee.ep.ProjectID, p => p.Id, (ee, p) => new { ee.e, ee.ep, p })
                .Join(_db.Departments, eep => eep.e.DepartmentId, d => d.Id, (eep, d) => new { eep.e, eep.p, d })
                .GroupBy(g => new { g.d.Name, g.p.Title })
                .Select(g => new
                {
                    DepartmentName = g.Key.Name,
                    ProjectTitle = g.Key.Title,
                    TotalBudget = g.Sum(x => x.p.Budget),
                    TotalSalaries = g.Sum(x => x.e.Salary)
                })
                .ToList();
            foreach (var result in repoert)
            {
                Console.WriteLine($"Department: {result.DepartmentName}, " +
                                  $"Project: {result.ProjectTitle}, " +
                                  $"Total Budget: {result.TotalBudget}, " +
                                  $"Total Salaries: {result.TotalSalaries}");
            }
        }
    }
}
