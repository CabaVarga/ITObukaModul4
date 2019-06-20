using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DataAccessContext context = new DataAccessContext();
        private GenericRepository<User> usersRepository;
        private GenericRepository<Offer> offersRepository;
        private GenericRepository<Category> categoriesRepository;
        private GenericRepository<Bill> billsRepository;
        private GenericRepository<Voucher> vouchersRepository;
        
        public GenericRepository<User> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new GenericRepository<User>(context);
                }
                return usersRepository;
            }
        }

        public GenericRepository<Offer> OffersRepository
        {
            get
            {
                if (this.offersRepository == null)
                {
                    this.offersRepository = new GenericRepository<Offer>(context);
                }
                return offersRepository;
            }
        }
        public GenericRepository<Category> CategoriesRepository
        {
            get
            {
                if (this.categoriesRepository == null)
                {
                    this.categoriesRepository = new GenericRepository<Category>(context);
                }
                return categoriesRepository;
            }
        }

        public GenericRepository<Bill> BillsRepository
        {
            get
            {
                if (this.billsRepository == null)
                {
                    this.billsRepository = new GenericRepository<Bill>(context);
                }
                return billsRepository;
            }
        }

        public GenericRepository<Voucher> VouchersRepository
        {
            get
            {
                if (this.vouchersRepository == null)
                {
                    this.vouchersRepository = new GenericRepository<Voucher>(context);
                }
                return vouchersRepository;
            }
        }

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