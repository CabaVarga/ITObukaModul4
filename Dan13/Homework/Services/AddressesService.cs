using Homework.Models;
using Homework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public class AddressesService : IAddressesService
    {
        private IUnitOfWork db;

        public AddressesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return db.AddressesRepository.Get();
        }

        public Address GetAddress(int id)
        {
            throw new NotImplementedException();
        }

        public Address CreateAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public Address UpdateAddress(int id, Address address)
        {
            throw new NotImplementedException();
        }

        public Address DeleteAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}