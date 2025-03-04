// See https://aka.ms/new-console-template for more information
using EmployeeSystem;
using EmployeeSystem.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static void Main(string[] args)
    {
        // Initialize the database context
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("\"Server=.;Database=EDB;Trusted_Connection=True;TrustServerCertificate=True\"") // Connecct to db
            .Options;

        var employeeRepository = new EmployeeRepository(options);
        Console.WriteLine("Task 1:"); // more than 3 project 
        employeeRepository.EmployeesWithMoreThan3Projects();

        Console.WriteLine("Task 2:");
        employeeRepository.findPreformance(); // bounce

        Console.WriteLine("Task3:");
        employeeRepository.fetchEmployees();

        Console.WriteLine("Task4:");
        employeeRepository.repoertWithEF();
        Console.WriteLine("Task4: Dapper version");
        employeeRepository.fetchreports();
    }

}