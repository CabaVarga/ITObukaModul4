using Project_3rd.Models;
using Project_3rd.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public class VoucherService : IVoucherService
    {
        private IUnitOfWork db;

        public VoucherService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<VoucherModel> GetAllVouchers()
        {
            return db.VouchersRepository.Get();
        }

        public VoucherModel GetVoucher(int id)
        {
            return db.VouchersRepository.GetByID(id);
        }

        public VoucherModel CreateVoucher(VoucherModel voucher)
        {
            // Mladen
            voucher.isUsed = false;
            voucher.expirationDate = DateTime.UtcNow.AddDays(7);

            db.VouchersRepository.Insert(voucher);
            db.Save();

            return voucher;
        }

        public VoucherModel UpdateVoucher(int id, VoucherModel voucher)
        {
            VoucherModel voucherToUpdate = db.VouchersRepository.GetByID(id);

            if (voucherToUpdate != null)
            {
                voucherToUpdate.isUsed = voucher.isUsed;
                voucherToUpdate.expirationDate = voucher.expirationDate;
                voucherToUpdate.offerModel = voucher.offerModel;
                voucherToUpdate.userModel = voucher.userModel;

                db.VouchersRepository.Update(voucherToUpdate);
                db.Save();
            }

            return voucherToUpdate;
        }

        public IEnumerable<VoucherModel> GetVouchersByBuyer(int buyerId)
        {
            return db.VouchersRepository.Get(x => x.userModel.id == buyerId);
        }

        public IEnumerable<VoucherModel> GetVouchersByOffer(int offerId)
        {
            return db.VouchersRepository.Get(x => x.offerModel.id == offerId);
        }

        public IEnumerable<VoucherModel> GetNonExpiredVouchers()
        {
            return db.VouchersRepository.Get(x => x.expirationDate < DateTime.UtcNow);
        }

        public VoucherModel CreateVoucherAfterBillPayment(BillModel bill)
        {
            VoucherModel voucher = new VoucherModel()
            {
                offerId = bill.offerId,
                userId = bill.userId,                
                isUsed = false,
                expirationDate = DateTime.UtcNow.AddDays(7)
            };

            db.VouchersRepository.Insert(voucher);
            db.Save();

            return voucher;
        }
    }
}