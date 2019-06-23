using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DataAccessContext context = new DataAccessContext();
        private GenericRepository<UserModel> userRepository;
        private GenericRepository<OfferModel> offerRepository;
        private GenericRepository<CategoryModel> categoryRepository;
        private GenericRepository<BillModel> billRepository;
        private GenericRepository<VoucherModel> voucherRepository;
        
        public GenericRepository<UserModel> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<UserModel>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<OfferModel> OfferRepository
        {
            get
            {
                if (this.offerRepository == null)
                {
                    this.offerRepository = new GenericRepository<OfferModel>(context);
                }
                return offerRepository;
            }
        }
        public GenericRepository<CategoryModel> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<CategoryModel>(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<BillModel> BillRepository
        {
            get
            {
                if (this.billRepository == null)
                {
                    this.billRepository = new GenericRepository<BillModel>(context);
                }
                return billRepository;
            }
        }

        public GenericRepository<VoucherModel> VoucherRepository
        {
            get
            {
                if (this.voucherRepository == null)
                {
                    this.voucherRepository = new GenericRepository<VoucherModel>(context);
                }
                return voucherRepository;
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