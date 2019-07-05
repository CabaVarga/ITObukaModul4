using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public interface IBillsService
    {
        IEnumerable<Bill> GetAllBills();
        Bill GetBill(int id);
        Bill UpdateBill(int id, Bill bill);
        Bill CreateBill(Bill bill);
        Bill DeleteBill(int id);
        IEnumerable<Bill> GetBillsByBuyer(int buyerId);
        IEnumerable<Bill> GetBillsByCategory(int categoryId);
        IEnumerable<Bill> GetBillsByDatePeriod(DateTime startDate, DateTime endDate);
        // 4.2.1
        IEnumerable<Bill> GetBillsByCategoryAndNotExpired(int categoryId, DateTime expiration);

        // 5.2.3
        ReportDTO GetSalesReportByDate(DateTime startDate, DateTime endDate);
        ReportDTO GetSalesReportByDateAndCategory(DateTime startDate, DateTime endDate, int categoryId);
    }
}