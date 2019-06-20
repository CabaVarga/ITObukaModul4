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
        private GenericRepository<UserModel> usersRepository;
        private GenericRepository<OfferModel> offersRepository;
        private GenericRepository<CategoryModel> categoriesRepository;
        private GenericRepository<BillModel> billsRepository;
        private GenericRepository<VoucherModel> vouchersRepository;
        
        public GenericRepository<UserModel> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new GenericRepository<UserModel>(context);
                }
                return usersRepository;
            }
        }

        public GenericRepository<OfferModel> OffersRepository
        {
            get
            {
                if (this.offersRepository == null)
                {
                    this.offersRepository = new GenericRepository<OfferModel>(context);
                }
                return offersRepository;
            }
        }
        public GenericRepository<CategoryModel> CategoriesRepository
        {
            get
            {
                if (this.categoriesRepository == null)
                {
                    this.categoriesRepository = new GenericRepository<CategoryModel>(context);
                }
                return categoriesRepository;
            }
        }

        public GenericRepository<BillModel> BillsRepository
        {
            get
            {
                if (this.billsRepository == null)
                {
                    this.billsRepository = new GenericRepository<BillModel>(context);
                }
                return billsRepository;
            }
        }

        public GenericRepository<VoucherModel> VouchersRepository
        {
            get
            {
                if (this.vouchersRepository == null)
                {
                    this.vouchersRepository = new GenericRepository<VoucherModel>(context);
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