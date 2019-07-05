using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models.DTOs.Reports
{
    public class ReportDTO
    {
        public ReportDTO()
        {
            reportItems = new List<ReportItemDTO>();
        }

        public decimal sumOfIncomes { get; set; }
        public int totalNumberOfSoldOffers { get; set; }
        public List<ReportItemDTO> reportItems { get; set; }
    }
}