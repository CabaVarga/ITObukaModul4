using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Reports
{
    public class ReportDto
    {
        public ReportDto()
        {
            reportItems = new List<ReportItemDto>();
        }

        public decimal sumOfIncomes { get; set; }
        public int totalNumberOfSoldOffers { get; set; }
        public List<ReportItemDto> reportItems { get; set; }
    }
}
