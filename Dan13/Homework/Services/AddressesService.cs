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
            return db.AddressesRepository.GetByID(id);
        }

        public Address CreateAddress(Address address)
        {
            db.AddressesRepository.Insert(address);
            db.Save();

            return address;
        }

        public Address UpdateAddress(int id, Address address)
        {
            Address updatedAddress = db.AddressesRepository.GetByID(id);

            if (updatedAddress != null)
            {
                updatedAddress.Street = address.Street;
                updatedAddress.City = address.City;
                updatedAddress.Country = address.Country;

                db.AddressesRepository.Update(updatedAddress);
                db.Save();
            }

            return updatedAddress;
        }

        public Address DeleteAddress(int id)
        {
            Address address = db.AddressesRepository.GetByID(id);

            if (address != null)
            {
                db.AddressesRepository.Delete(address);
                db.Save();
            }

            return address;
        }
    }
}