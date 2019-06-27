using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public interface IVoucherService
    {
        IEnumerable<VoucherModel> GetAllVouchers();
        VoucherModel GetVoucher(int id);
        VoucherModel CreateVoucher(VoucherModel voucher);
        VoucherModel UpdateVoucher(int id, VoucherModel voucher);
        IEnumerable<VoucherModel> GetVouchersByBuyer(int buyerId);
        IEnumerable<VoucherModel> GetVouchersByOffer(int offerId);
        IEnumerable<VoucherModel> GetNonExpiredVouchers();
        VoucherModel CreateVoucherAfterBillPayment(BillModel bill);
    }
}