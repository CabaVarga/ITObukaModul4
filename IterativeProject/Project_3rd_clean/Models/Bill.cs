using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models
{
    public class Bill
    {
        [Key]
        public int id { get; set; }

        public bool paymentMade { get; set; }

        public bool paymentCancelled { get; set; }

        public DateTime billCreated { get; set; } = DateTime.UtcNow;

        [ForeignKey("offerModel")]
        public int? offerId { get; set; }

        [ForeignKey("userModel")]
        public int? userId { get; set; }

        // Navigation properties:
        public virtual Offer offerModel { get; set; }

        public virtual User userModel { get; set; }
    }
}