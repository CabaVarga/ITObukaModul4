using Project_3rd.Models;
using Project_3rd.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public class BillService : IBillService
    {
        private IUnitOfWork db;

        public BillService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<BillModel> GetAllBills()
        {
            return db.BillsRepository.Get();
        }

        public BillModel GetBill(int id)
        {
            return db.BillsRepository.GetByID(id);
        }

        public BillModel UpdateBill(int id, BillModel bill)
        {
            BillModel updatedBill = db.BillsRepository.GetByID(id);

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

        public BillModel CreateBill(BillModel bill)
        {
            // Business logic
            bill.paymentMade = false;
            bill.paymentCancelled = false;

            db.BillsRepository.Insert(bill);
            db.Save();

            return bill;
        }

        public BillModel DeleteBill(int id)
        {
            BillModel bill = db.BillsRepository.GetByID(id);

            if (bill != null)
            {
                // HMMMM if we make changes in offer when we are creating
                // the bill, shouldn't we also update offer when we delete the bill???
                db.BillsRepository.Delete(bill);
                db.Save();
            }

            return bill;
        }

        public IEnumerable<BillModel> GetBillsByBuyer(int buyerId)
        {
            return db.BillsRepository.Get(
                filter: b => b.userId == buyerId);
        }

        public IEnumerable<BillModel> GetBillsByCategory(int categoryId)
        {
            return db.BillsRepository.Get(
                filter: b => b.offerModel.categoryId == categoryId,
                includeProperties: "offerModel");

            // Mladen's solution:
            // his navigation properties are virtual so he couldn't use Include
            // my solution will work only if they are not virtual
            // return db.BillsRepository.Get(x => x.Category.ID == categoryId);
        }

        public IEnumerable<BillModel> GetBillsByDatePeriod(DateTime startDate, DateTime endDate)
        {
            return db.BillsRepository.Get(x => x.billCreated >= startDate && x.billCreated <= endDate);
        }
    }
}