using Project_3rd_clean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public interface IVouchersService
    {
        IEnumerable<Voucher> GetAllVouchers();
        Voucher GetVoucher(int id);
        Voucher CreateVoucher(Voucher voucher);
        Voucher UpdateVoucher(int id, Voucher voucher);
        IEnumerable<Voucher> GetVouchersByBuyer(int buyerId);
        IEnumerable<Voucher> GetVouchersByOffer(int offerId);
        IEnumerable<Voucher> GetNonExpiredVouchers();
        Voucher CreateVoucherAfterBillPayment(Bill bill);
    }
}