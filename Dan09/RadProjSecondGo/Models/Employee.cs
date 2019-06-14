using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadProjSecondGo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public decimal? Bonus { get; set; }
        public DateTime BirthDay { get; set; }

        // Navigation properties
        public virtual Employee Manager { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }

    }
}