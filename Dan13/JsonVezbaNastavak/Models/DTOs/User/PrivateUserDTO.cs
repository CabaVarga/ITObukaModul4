using JsonVezbaNastavak.Models.DTOs.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonVezbaNastavak.Models.DTOs.User
{
    public class PrivateUserDTO : PublicUserDTO
    {
        public PrivateAddressDTO Address { get; set; }
    }
}