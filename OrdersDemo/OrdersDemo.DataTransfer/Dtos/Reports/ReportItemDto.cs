using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Reports
{
    public class ReportItemDto
    {
        public DateTime date { get; set; }
        public decimal income { get; set; }
        public string categoryName { get; set; }
        public int numberOfOffers { get; set; }
    }
}
