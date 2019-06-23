using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_2nd.Models
{
    public class BillModel
    {
        [Key]
        public int id { get; set; }

        public bool paymentMade { get; set; }

        public bool paymentCancelled { get; set; }

        public DateTime billCreated { get; set; }

        // Navigation properties:
        public virtual OfferModel offerModel { get; set; }

        public virtual UserModel userModel { get; set; }
    }
}