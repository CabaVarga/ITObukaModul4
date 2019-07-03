using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models.DTOs.Voucher
{
    public class AdminVoucherDTO : PrivateVoucherDTO
    {
        public bool IsUsed { get; set; }
    }
}