using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.Reports;
using Project_3rd_clean.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        // ZADACI 5.2.3 i 5.2.4
        public ReportDTO GetSalesReportByDate(DateTime startDate, DateTime endDate)
        {
            // return only bills in a given date range
            var bills = db.BillsRepository.Get(filter: b => b.billCreated >= startDate && b.billCreated <= endDate);

            // Instead of using nested groupings a simple grouping on multiple properties will suffice...
            var grouped =
                from bill in bills
                group bill by new { bill.billCreated, bill.offerModel.category.category_name } into grouping
                select new ReportItemDTO()
                {
                    categoryName = grouping.Key.category_name,
                    date = grouping.Key.billCreated,
                    income = grouping.Select(b => b.offerModel.regular_price).Sum(),
                    numberOfOffers = grouping.Select(b => b).Count()
                };

            #region Failed attempts - no need for nested grouping ...
            //var prodaja  = from b in db.BillsRepository.Get(filter: b => b.billCreated >= startDate && b.billCreated <= endDate)
            //               group b by b.billCreated into g
            //               select new ReportItemDTO()
            //               {
            //                   date = g.Select(a => a.billCreated).FirstOrDefault(),
            //                   categoryName = null,
            //                   income = g.Select(a => a.offerModel.regular_price).Sum(),
            //                   numberOfOffers = g.Select(a => a.offerModel).Count()
            //               };

            //var step1 = from b in db.BillsRepository.Get(filter: b => b.billCreated >= startDate && b.billCreated <= endDate)
            //            group b by b.billCreated into g
            //            select g.Select(row => row);

            //var step2 = from b in step1
            //            group b by b.Select(row => row.offerModel.category.category_name) into wow
            //            select new ReportItemDTO()
            //            {
            //                date = wow.Select(a => a.Select(b => b.billCreated).FirstOrDefault()).FirstOrDefault(),
            //                categoryName = wow.Key.FirstOrDefault(),
            //                // categoryName = wow.Select(a => a.Select(b => b.offerModel.category.category_name).FirstOrDefault()).FirstOrDefault(),
            //                income = wow.Select(a => a.Select(b => b.offerModel.regular_price).Sum()).FirstOrDefault(),
            //                numberOfOffers = wow.Select(a => a.Select(b => b.offerModel.regular_price).Count()).FirstOrDefault()
            //            };
                       
            //var alt2 = bills
            //    .GroupBy(b => b.billCreated)
            //    .Select(b => b);

            //var alt3 =
            //    from bill in bills
            //    group bill by bill.billCreated into billsByDate
            //    from billsByCategory in 
            //    (
            //        from bill in billsByDate
            //        group bill by bill.offerModel.categoryId
            //    )
            //    group billsByCategory by billsByDate.Key;

            //var alt4 = bills
            //    .GroupBy(bill => bill.billCreated)
            //    .SelectMany(
            //        billsByDate => billsByDate.GroupBy(bill => bill.offerModel.category)
            //        , (billByDate, categoryGroup) =>
            //        new
            //        {
            //            billByDate,
            //            categoryGroup
            //        })
            //    .GroupBy(item => item.billByDate.Key, item => item.categoryGroup);

            //var alt33 = bills
            //    .GroupBy(bill => bill.billCreated)
            //    .Select(billsByDate => billsByDate
            //        .GroupBy(billByDate => billByDate.offerModel.category.id)
            //        .Select(billByDateAndCategory =>
            //            new ReportItemDTO()
            //            {
            //                categoryName = billByDateAndCategory.Select(a => a.offerModel.category.category_name).FirstOrDefault(),
            //                date = billByDateAndCategory.Select(a => a.billCreated).FirstOrDefault(),
            //                income = billByDateAndCategory.Select(a => a.offerModel.regular_price).Sum(),
            //                numberOfOffers = billByDateAndCategory.Select(a => a).Count()
            //            }));

            //var alt5 = bills
            //    .GroupBy(bill => bill.billCreated)
            //    .SelectMany(
            //        billsByDate => billsByDate.GroupBy(bill => bill.offerModel.category)
            //        , (billByDate, categoryGroup) =>
            //        new ReportItemDTO()
            //        {
            //            categoryName = categoryGroup.Key.category_name,
            //            date = billByDate.Key,
            //            income = categoryGroup.Select(yay => yay.offerModel.regular_price).Sum(),
            //            numberOfOffers = categoryGroup.Select(yay => yay).Count()
            //        })
            //    .GroupBy(item => item.date, item => item.categoryName);

            // -------------------------------

            //var queryNestedGroups =
            //    from bill in bills
            //    group bill by bill.billCreated into newGroup1
            //    from newGroup2 in
            //        (from bill in newGroup1
            //         group bill by bill.offerModel.category.category_name)
            //    group newGroup2 by newGroup1.Key;

            //foreach (var outerGroup in queryNestedGroups)
            //{
            //    Debug.WriteLine($"Bill Level = {outerGroup.Key}");
            //    foreach(var innerGroup in outerGroup)
            //    {
            //        Debug.WriteLine($"Bills created at: {innerGroup.Key}");
            //        foreach(var innerGroupElement in innerGroup)
            //        {
            //            Debug.WriteLine(innerGroupElement.id);
            //        }
            //    }
            //}
            #endregion

            ReportDTO report = new ReportDTO()
            {
                reportItems = grouped.OrderBy(b => b.date).ThenBy(b => b.categoryName).ToList(),
                sumOfIncomes = grouped.Select(p => p.income).Sum(),
                totalNumberOfSoldOffers = grouped.Select(p => p.numberOfOffers).Sum()
            };

            return report;
        }

        public ReportDTO GetSalesReportByDateAndCategory(DateTime startDate, DateTime endDate, int categoryId)
        {
            // FIRST: SELECT BY BOTH DATE AND CATEGORY
            var prodaja = from b in db.BillsRepository.Get(filter: b => b.offerModel.categoryId == categoryId && b.billCreated >= startDate && b.billCreated <= endDate)
                          group b by b.billCreated into g
                          select g.Select(row => row);

            // SECOND: GROUP BY DATE
            var prodaja2 = from p in prodaja
                           select new ReportItemDTO()
                           {
                               date = p.Select(a => a.billCreated).FirstOrDefault(),
                               categoryName = p.Select(a => a.offerModel.category.category_name).FirstOrDefault(),
                               income = p.Select(a => a.offerModel.regular_price).Sum(),
                               numberOfOffers = p.Select(a => a.offerModel).Count()
                           };

            ReportDTO report = new ReportDTO()
            {
                reportItems = prodaja2.OrderBy(b => b.date).ToList(),
                sumOfIncomes = prodaja2.Select(p => p.income).Sum(),
                totalNumberOfSoldOffers = prodaja2.Select(p => p.numberOfOffers).Sum()
            };

            return report;
        }
    }
}