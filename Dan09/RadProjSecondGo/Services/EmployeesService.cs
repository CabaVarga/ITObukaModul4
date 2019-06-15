using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RadProjSecondGo.Models;
using RadProjSecondGo.Repositories;

namespace RadProjSecondGo.Services
{
    public class EmployeesService : IEmployeesService
    {
        private IUnitOfWork db;

        public EmployeesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public Employee CreateEmployee(Employee employee)
        {
            db.EmployeeRepository.Insert(employee);
            db.Save();

            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = db.EmployeeRepository.GetByID(id);

            if (employee != null)
            {
                db.EmployeeRepository.Delete(employee);
                db.Save();
            }

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return db.EmployeeRepository.Get();
        }

        public Employee GetEmployee(int id)
        {
            return db.EmployeeRepository.GetByID(id);
        }

        public Employee UpdateEmployee(int id, Employee employee)
        {
            Employee updatedEmployee = db.EmployeeRepository.GetByID(id);

            if (updatedEmployee != null)
            {
                updatedEmployee.IdentityNumber = employee.IdentityNumber;
                updatedEmployee.FirstName = employee.FirstName;
                updatedEmployee.LastName = employee.LastName;
                updatedEmployee.Salary = employee.Salary;
                updatedEmployee.Bonus = employee.Bonus;
                updatedEmployee.BirthDay = employee.BirthDay;
                updatedEmployee.Manager = employee.Manager;

                db.EmployeeRepository.Update(updatedEmployee);
                db.Save();
            }

            return updatedEmployee;
        }
    }
}