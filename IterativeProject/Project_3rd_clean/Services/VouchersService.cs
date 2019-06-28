using Project_3rd_clean.Models;
using Project_3rd_clean.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public class VouchersService : IVouchersService
    {
        private IUnitOfWork db;

        public VouchersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Voucher> GetAllVouchers()
        {
            return db.VouchersRepository.Get();
        }

        public Voucher GetVoucher(int id)
        {
            return db.VouchersRepository.GetByID(id);
        }

        public Voucher CreateVoucher(Voucher voucher)
        {
            // Mladen
            voucher.isUsed = false;
            voucher.expirationDate = DateTime.UtcNow.AddDays(7);

            db.VouchersRepository.Insert(voucher);
            db.Save();

            return voucher;
        }

        public Voucher UpdateVoucher(int id, Voucher voucher)
        {
            Voucher voucherToUpdate = db.VouchersRepository.GetByID(id);

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

        public IEnumerable<Voucher> GetVouchersByBuyer(int buyerId)
        {
            return db.VouchersRepository.Get(x => x.userModel.id == buyerId);
        }

        public IEnumerable<Voucher> GetVouchersByOffer(int offerId)
        {
            return db.VouchersRepository.Get(x => x.offerModel.id == offerId);
        }

        public IEnumerable<Voucher> GetNonExpiredVouchers()
        {
            return db.VouchersRepository.Get(x => x.expirationDate < DateTime.UtcNow);
        }

        public Voucher CreateVoucherAfterBillPayment(Bill bill)
        {
            Voucher voucher = new Voucher()
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