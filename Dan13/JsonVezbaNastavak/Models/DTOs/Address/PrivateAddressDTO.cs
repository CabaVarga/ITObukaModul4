﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonVezbaNastavak.Models.DTOs.Address
{
    public class PrivateAddressDTO : PublicAddressDTO
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}