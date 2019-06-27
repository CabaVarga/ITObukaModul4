using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public interface IBillService
    {
        IEnumerable<BillModel> GetAllBills();
        BillModel GetBill(int id);
        BillModel UpdateBill(int id, BillModel bill);
        BillModel CreateBill(BillModel bill);
        BillModel DeleteBill(int id);
        IEnumerable<BillModel> GetBillsByBuyer(int buyerId);
        IEnumerable<BillModel> GetBillsByCategory(int categoryId);
        IEnumerable<BillModel> GetBillsByDatePeriod(DateTime startDate, DateTime endDate);
        // 4.2.1
        IEnumerable<BillModel> GetBillsByCategoryAndNotExpired(int categoryId, DateTime expiration);
    }
}