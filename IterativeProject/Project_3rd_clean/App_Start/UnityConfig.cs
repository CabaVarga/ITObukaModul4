using Project_3rd_clean.Models;
using Project_3rd_clean.Repositories;
using Project_3rd_clean.Services;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Project_3rd_clean
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
            container.RegisterType<IGenericRepository<Category>, GenericRepository<Category>>();
            container.RegisterType<IGenericRepository<Offer>, GenericRepository<Offer>>();
            container.RegisterType<IGenericRepository<Voucher>, GenericRepository<Voucher>>();
            container.RegisterType<IGenericRepository<Bill>, GenericRepository<Bill>>();

            container.RegisterType<DbContext, DataAccessContext>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IOffersService, OffersService>();
            container.RegisterType<ICategoriesService, CategoriesService>();
            container.RegisterType<IBillsService, BillsService>();
            container.RegisterType<IVouchersService, VouchersService>();
            container.RegisterType<IEmailsService, EmailsService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}