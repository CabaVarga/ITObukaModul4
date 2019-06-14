using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadProjSecondGo.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string Contractor { get; set; }

        // Navigation properties
        public virtual Employee ProjectManager { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}