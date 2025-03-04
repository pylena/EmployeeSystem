using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Deadline { get; set; }
        public double Budget { get; set; }
        //   for Many-to-Many
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
