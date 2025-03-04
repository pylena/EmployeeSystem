﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlServer("Server=.;Database=EDB;Trusted_Connection=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
           .HasKey(ep => new { ep.EmployeeID, ep.ProjectID });

            modelBuilder.Entity<EmployeeProject>()
           .HasOne(ep => ep.Employee)
           .WithMany(e => e.EmployeeProjects)
           .HasForeignKey(ep => ep.EmployeeID);

            modelBuilder.Entity<EmployeeProject>()
           .HasOne(ep => ep.Project)
           .WithMany(p => p.EmployeeProjects)
           .HasForeignKey(ep => ep.ProjectID);
        }


    }
}
