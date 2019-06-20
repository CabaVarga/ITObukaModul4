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
        IEnumerable<VoucherModel> GetAllVouchers();

        VoucherModel GetVoucher(int id);

        VoucherModel CreateVoucher(VoucherModel voucher);

        VoucherModel UpdateVoucher(int id, string name, string email, string city);

        VoucherModel DeleteVoucher(int id);
    }
}
