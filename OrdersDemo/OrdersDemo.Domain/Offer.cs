using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.Domain
{
    public class Offer
    {
        [Key]
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

        // A HACK:
        // [ForeignKey("userModel")]
        // public int? offer_created_by { get; set; }

        public EOfferStatus offer_status { get; set; }

        [ForeignKey("seller")]
        public int? sellerId { get; set; }

        [ForeignKey("category")]
        public int? categoryId { get; set; }

        // Navigation properties
        // WITHOUT VIRTUAL JSON WON'T SERIALIZE THE RELATED ENTITIES?
        public virtual Category category { get; set; }

        public virtual User seller { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bill> billModels { get; set; }

        [JsonIgnore]
        public virtual ICollection<Voucher> voucherModels { get; set; }
    }
}
