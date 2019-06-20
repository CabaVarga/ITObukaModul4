using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class Bill
    {
        [Key]
        public int id { get; set; }

        public bool paymentMade { get; set; }

        public bool paymentCancelled { get; set; }

        public DateTime billCreated { get; set; }

        // Navigation properties:
        public virtual Offer offerModel { get; set; }

        public virtual User userModel { get; set; }
    }
}