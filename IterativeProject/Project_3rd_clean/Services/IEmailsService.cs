using Project_3rd_clean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public interface IEmailsService
    {
        void SendEmail(Voucher voucher);
    }
}