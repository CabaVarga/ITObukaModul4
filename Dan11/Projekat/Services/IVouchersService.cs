using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Services
{
    interface IVouchersService
    {
        IEnumerable<Voucher> GetAllVouchers();

        Voucher GetVoucher(int id);

        Voucher CreateVoucher(Voucher voucher);

        Voucher UpdateVoucher(int id, string name, string email, string city);

        Voucher DeleteVoucher(int id);
    }
}
