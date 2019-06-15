using RadProjSecondGo.Models;
using RadProjSecondGo.Repositories;
using RadProjSecondGo.Services;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace RadProjSecondGo
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IGenericRepository<Employee>, GenericRepository<Employee>>();
            container.RegisterType<IGenericRepository<Project>, GenericRepository<Project>>();
            container.RegisterType<IGenericRepository<EmployeeProject>, GenericRepository<EmployeeProject>>();

            container.RegisterType<DbContext, ProjectContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IEmployeesService, EmployeesService>();
            container.RegisterType<IProjectsService, ProjectsService>();
            container.RegisterType<IEmployeeProjectsService, EmployeeProjectsService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}