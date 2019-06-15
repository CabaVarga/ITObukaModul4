using RadProjSecondGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadProjSecondGo.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Project> ProjectsRepository { get; }

        IGenericRepository<Employee> EmployeeRepository { get; }

        IGenericRepository<EmployeeProject> EmployeeProjectRepository { get; }

        void Save();
    }
}
