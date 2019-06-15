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

            IList<Employee> employees = new List<Employee>
            {
                new Employee() { IdentityNumber = "10", FirstName = "Pera", LastName = "Peric", Salary = 10000, Bonus = 100, BirthDay = new DateTime(1987, 1, 1), Manager = null },
                new Employee() { IdentityNumber = "20", FirstName = "Milan", LastName = "Milic", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "30", FirstName = "Eva", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "40", FirstName = "Moma", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "50", FirstName = "Rastko", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "60", FirstName = "Mira", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "70", FirstName = "Ivan", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "80", FirstName = "Petar", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "90", FirstName = "Marko", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "100", FirstName = "Jova", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "110", FirstName = "Laza", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "120", FirstName = "Djoka", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "130", FirstName = "Ziva", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "140", FirstName = "Iva", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "150", FirstName = "Seka", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "160", FirstName = "Ruza", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "170", FirstName = "Ana", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "180", FirstName = "Eva", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "190", FirstName = "Eva", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "200", FirstName = "Ana", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "210", FirstName = "Ana", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null },
                new Employee() { IdentityNumber = "220", FirstName = "Ena", LastName = "", Salary = 0, Bonus = 0, BirthDay = new DateTime(2012, 3, 12), Manager = null }
            };

            context.Employees.AddRange(employees);

            // Populate Projects


            // Populate EmployeeProjects

            base.Seed(context);
        }
    }
}