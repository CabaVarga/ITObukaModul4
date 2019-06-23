using Project_3rd.Models;
using Project_3rd.Repositories;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Project_3rd
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
            container.RegisterType<IGenericRepository<UserModel>, GenericRepository<UserModel>>();
            container.RegisterType<IGenericRepository<CategoryModel>, GenericRepository<CategoryModel>>();
            container.RegisterType<IGenericRepository<OfferModel>, GenericRepository<OfferModel>>();
            container.RegisterType<IGenericRepository<VoucherModel>, GenericRepository<VoucherModel>>();
            container.RegisterType<IGenericRepository<BillModel>, GenericRepository<BillModel>>();

            container.RegisterType<DbContext, DataAccessContext>(
                new HierarchicalLifetimeManager());
                                 
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}