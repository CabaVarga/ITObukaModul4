using RadProjSecondGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadProjSecondGo.Services
{
    public interface IEmployeesService
    {
        IEnumerable<Employee> GetAllEmployees();

        Employee CreateEmployee(Employee employee);

        Employee UpdateEmployee(int id, Employee employee);

        Employee DeleteEmployee(int id);

        Employee GetEmployee(int id);
    }
}
