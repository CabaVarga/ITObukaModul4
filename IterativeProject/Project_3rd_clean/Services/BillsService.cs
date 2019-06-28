using Project_3rd_clean.Models;
using Project_3rd_clean.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public class BillsService : IBillsService
    {
        private IUnitOfWork db;

        public BillsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Bill> GetAllBills()
        {
            return db.BillsRepository.Get();
        }

        public Bill GetBill(int id)
        {
            return db.BillsRepository.GetByID(id);
        }

        public Bill UpdateBill(int id, Bill bill)
        {
            Bill updatedBill = db.BillsRepository.GetByID(id);

            // Kreiranje racuna ovde ili u kontroleru???
            if (updatedBill != null)
            {
                updatedBill.paymentMade = bill.paymentMade;
                updatedBill.paymentCancelled = bill.paymentCancelled;
                updatedBill.offerModel = bill.offerModel;
                updatedBill.userModel = bill.userModel;

                db.BillsRepository.Update(updatedBill);
                db.Save();
            }

            return updatedBill;
        }

        public Bill CreateBill(Bill bill)
        {
            // Business logic
            bill.paymentMade = false;
            bill.paymentCancelled = false;

            db.BillsRepository.Insert(bill);
            db.Save();

            return bill;
        }

        public Bill DeleteBill(int id)
        {
            Bill bill = db.BillsRepository.GetByID(id);

            if (bill != null)
            {
                // HMMMM if we make changes in offer when we are creating
                // the bill, shouldn't we also update offer when we delete the bill???
                db.BillsRepository.Delete(bill);
                db.Save();
            }

            return bill;
        }

        public IEnumerable<Bill> GetBillsByBuyer(int buyerId)
        {
            return db.BillsRepository.Get(
                filter: b => b.userId == buyerId);
        }

        public IEnumerable<Bill> GetBillsByCategory(int categoryId)
        {
            return db.BillsRepository.Get(
                filter: b => b.offerModel.categoryId == categoryId,
                includeProperties: "offerModel");

            // Mladen's solution:
            // his navigation properties are virtual so he couldn't use Include
            // my solution will work only if they are not virtual
            // return db.BillsRepository.Get(x => x.Category.ID == categoryId);
        }

        public IEnumerable<Bill> GetBillsByDatePeriod(DateTime startDate, DateTime endDate)
        {
            return db.BillsRepository.Get(x => x.billCreated >= startDate && x.billCreated <= endDate);
        }

        public IEnumerable<Bill> GetBillsByCategoryAndNotExpired(int categoryId, DateTime expiration)
        {
            return db.BillsRepository.Get(
                filter: b => b.offerModel.categoryId == categoryId && b.offerModel.offer_expires < expiration,
                includeProperties: "offerModel");
        }
    }
}