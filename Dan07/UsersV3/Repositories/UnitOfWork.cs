using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersV3.Models;

namespace UsersV3.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DataAccessContext context = new DataAccessContext();
        private GenericRepository<User> userRepository;
        private GenericRepository<Address> addressRepository;
        private GenericRepository<City> cityRepository;
        private GenericRepository<Country> countryRepository;

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

        public GenericRepository<Address> AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new GenericRepository<Address>(context);
                }
                return addressRepository;
            }
        }
        public GenericRepository<City> CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                {
                    this.cityRepository = new GenericRepository<City>(context);
                }
                return cityRepository;
            }
        }
        public GenericRepository<Country> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                {
                    this.countryRepository = new GenericRepository<Country>(context);
                }
                return countryRepository;
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