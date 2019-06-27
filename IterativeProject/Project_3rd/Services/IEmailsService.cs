using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public interface IEmailsService
    {
        void SendEmail(VoucherModel voucher);
    }
}