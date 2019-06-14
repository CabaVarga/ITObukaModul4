using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RadProjSecondGo.Models
{
    public class ProjectContextInitializer : DropCreateDatabaseAlways<ProjectContext>
    {
        protected override void Seed(ProjectContext context)
        {
            // Populate Employees

            IList<Employee> employees = new List<Employee>();

            employees.Add(new Employee() { IdentityNumber = "10",   FirstName = "Pera",     LastName = "Peric", Salary = 10000, Bonus = 100, BirthDay = new DateTime(1987, 1, 1), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "20",   FirstName = "Milan",    LastName = "Milic", Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "30",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "40",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "50",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "60",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "70",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "80",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "90",   FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "100",  FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "110",  FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });
            employees.Add(new Employee() { IdentityNumber = "120",  FirstName = "",         LastName = "",      Salary = 0,     Bonus = 0,  BirthDay = new DateTime(2012, 3, 12), Manager = null });


            // Populate Projects


            // Populate EmployeeProjects

            base.Seed(context);
        }
    }
}