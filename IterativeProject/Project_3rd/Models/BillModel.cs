using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_3rd.Models
{
    public class BillModel
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
        public virtual OfferModel offerModel { get; set; }

        public virtual UserModel userModel { get; set; }
    }
}