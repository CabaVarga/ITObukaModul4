﻿using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DataAccessContext context = new DataAccessContext();
        private GenericRepository<UserModel> userRepository;
        private GenericRepository<OfferModel> offerRepository;
        private GenericRepository<CategoryModel> categoryRepository;
        
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