using Homework.Models.DTOs.Account;
using Homework.Models.DTOs.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models.DTOs.User
{
    public class PrivateUserDTO : PublicUserDTO
    {
        public PrivateAddressDTO Address { get; set; }

        // IEnumerable almost certainly will not work....
        public IEnumerable<PrivateAccountDTO> Accounts { get; set; }
    }
}