using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public interface IAddressesService
    {
        #region CRUD
        IEnumerable<Address> GetAllAddresses();

        Address GetAddress(int id);

        Address CreateAddress(Address address);

        Address UpdateAddress(int id, Address address);

        Address DeleteAddress(int id);
        #endregion
    }
}