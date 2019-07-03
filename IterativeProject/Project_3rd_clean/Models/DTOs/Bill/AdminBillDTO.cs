using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models.DTOs.Bill
{
    public class AdminBillDTO : PrivateBillDTO
    {
        public bool PaymentMade { get; set; }
        public bool PaymentCancelled { get; set; }
    }
}