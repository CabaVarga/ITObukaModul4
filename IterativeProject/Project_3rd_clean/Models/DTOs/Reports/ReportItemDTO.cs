using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models.DTOs.Reports
{
    public class ReportItemDTO
    {
        public DateTime date { get; set; }
        public decimal income { get; set; }
        public string categoryName { get; set; }
        public int numberOfOffers { get; set; }
    }
}