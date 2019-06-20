using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class VoucherModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime expirationDate { get; set; }

        public bool isUsed { get; set; }

        // Navigation properties
        public virtual OfferModel offerModel { get; set; }

        public virtual UserModel userModel { get; set; }
    }
}