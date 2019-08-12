using OrdersDemo.Domain;
using OrdersDemo.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace OrdersDemo.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        [Dependency]
        public IGenericRepository<User> UsersRepository { get; set; }

        [Dependency]
        public IGenericRepository<Category> CategoriesRepository { get; set; }

        [Dependency]
        public IGenericRepository<Offer> OffersRepository { get; set; }

        [Dependency]
        public IGenericRepository<Bill> BillsRepository { get; set; }

        [Dependency]
        public IGenericRepository<Voucher> VouchersRepository { get; set; }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
