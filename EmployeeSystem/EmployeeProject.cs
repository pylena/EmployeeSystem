﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem
{
    public class EmployeeProject
    {
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
