using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RadProjSecondGo.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public int HoursPerWeek { get; set; }

        // Navigation properties
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}