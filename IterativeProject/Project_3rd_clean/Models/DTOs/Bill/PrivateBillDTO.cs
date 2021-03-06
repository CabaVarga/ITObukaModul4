﻿using Project_3rd_clean.Models.DTOs.Offer;
using Project_3rd_clean.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models.DTOs.Bill
{
    public class PrivateBillDTO : PublicBillDTO
    {
        public PrivateUserDTO Buyer { get; set; }

        public PublicOfferDTO Offer { get; set; }
    }
}