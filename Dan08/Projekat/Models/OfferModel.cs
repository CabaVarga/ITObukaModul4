using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class OfferModel
    {
        public enum OfferStatus { WAIT_FOR_APPROVING, APPROVED, DECLINED, EXPIRED };

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string offer_name { get; set; }

        [Required]
        [StringLength(200)]
        public string offer_description { get; set; }

        public DateTime offer_created { get; set; }

        public DateTime offer_expires { get; set; }

        public decimal regular_price { get; set; }

        public decimal action_price { get; set; }

        public string image_path { get; set; }

        public int available_offers { get; set; }

        public int bought_offers { get; set; }

        public OfferStatus offer_status { get; set; }
    }
}