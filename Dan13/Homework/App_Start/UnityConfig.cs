using Homework.Models;
using Homework.Repositories;
using Homework.Services;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Homework
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IGenericRepository<User>, GenericRepository<User>>();
            container.RegisterType<IGenericRepository<Address>, GenericRepository<Address>>();
            container.RegisterType<IGenericRepository<Account>, GenericRepository<Account>>();


            container.RegisterType<DbContext, DataAccessContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IAddressesService, AddressesService>();
            container.RegisterType<IAccountsService, AccountsService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}