using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RadProjSecondGo.Models;
using RadProjSecondGo.Repositories;

namespace RadProjSecondGo.Services
{
    public class EmployeeProjectsService : IEmployeeProjectsService
    {
        private IUnitOfWork db;

        public EmployeeProjectsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public EmployeeProject CreateEmployeeProject(EmployeeProject employeeProject)
        {
            db.EmployeeProjectRepository.Insert(employeeProject);
            db.Save();

            return employeeProject;
        }

        public EmployeeProject DeleteEmployeeProject(int id)
        {
            EmployeeProject employeeProject = db.EmployeeProjectRepository.GetByID(id);

            if (employeeProject != null)
            {
                db.EmployeeProjectRepository.Delete(employeeProject);
                db.Save();
            }

            return employeeProject;
        }

        public IEnumerable<EmployeeProject> GetAllEmployeeProjects()
        {
            return db.EmployeeProjectRepository.Get();
        }

        public EmployeeProject GetEmployeeProject(int id)
        {
            return db.EmployeeProjectRepository.GetByID(id);
        }

        public EmployeeProject UpdateEmployeeProject(int id, EmployeeProject employeeProject)
        {
            EmployeeProject updatedEmployeeProject = db.EmployeeProjectRepository.GetByID(id);

            if (updatedEmployeeProject != null)
            {
                updatedEmployeeProject.HoursPerWeek = employeeProject.HoursPerWeek;
                db.Save();
            }

            return updatedEmployeeProject;
        }
    }
}