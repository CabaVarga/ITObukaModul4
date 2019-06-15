using RadProjSecondGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadProjSecondGo.Services
{
    public interface IEmployeeProjectsService
    {
        IEnumerable<EmployeeProject> GetAllEmployeeProjects();

        EmployeeProject CreateEmployeeProject(EmployeeProject employeeProject);

        EmployeeProject UpdateEmployeeProject(int id, EmployeeProject employeeProject);

        EmployeeProject DeleteEmployeeProject(int id);

        EmployeeProject GetEmployeeProject(int id);
    }
}
