using rep_uow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rep_uow.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DataAccessContext context = new DataAccessContext();
        private GenericRepository<User> userRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
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