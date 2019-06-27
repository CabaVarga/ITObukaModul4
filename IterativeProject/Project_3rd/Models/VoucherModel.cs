using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_3rd.Models
{
    public class VoucherModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime expirationDate { get; set; }

        public bool isUsed { get; set; }

        [ForeignKey("offerModel")]
        public int? offerId { get; set; }

        [ForeignKey("userModel")]
        public int? userId { get; set; }

        // Navigation properties
        public virtual OfferModel offerModel { get; set; }

        public virtual UserModel userModel { get; set; }
    }
}