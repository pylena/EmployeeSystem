# Overview
* This project implements a simple Employee and Project Management System using Entity Framework Core (EF Core) and Dapper. 
The system features both one-to-many and many-to-many relationships between the entities and includes functionality for querying. It also showcases the usage of stored procedures for employee bonus calculations.

## Key Implementation:
* Dapper Optimization for Employee and Project Data Retrieval
To improve the performance of data retrieval, Dapper was used to fetch essential columns (EmployeeName, ProjectTitle, and ProjectDeadline)
for employees and their assigned projects.
This approach uses Dapper to avoid loading unnecessary data and is particularly useful for large datasets.(egger loading)

## Entity Framework vs Dapper for Financial Reports
two versions are implemented for fetching financial reports (total department salaries and project budgets)
* EF Core may not be as fast as Dapper for complex queries, especially when dealing with large datasets. This is due to the overhead of tracking entities, generating SQL dynamically, and handling lazy loading.
* Dapper is generally faster than EF Core for read-heavy operations

### Technologies Used
* Entity Framework Core 
* Dapper for optimized database queries
* SQL Server as the relational database management system
* .NET Core for building the console application
* LINQ for querying data in EF Core



