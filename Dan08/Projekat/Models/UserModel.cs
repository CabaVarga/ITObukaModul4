using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class UserModel
    {
        public enum UserRoles { ROLE_CUSTOMER, ROLE_ADMIN, ROLE_SELLER };

        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string first_name { get; set; }

        [Required]
        [StringLength(45)]
        public string last_name { get; set; }

        [Required]
        [StringLength(45)]
        public string username { get; set; }

        [Required]
        [StringLength(45)]
        public string password { get; set; }

        [Required]
        [StringLength(45)]
        public string email { get; set; }

        public UserRoles user_role { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<OfferModel> offerModels { get; set; }

        [JsonIgnore]
        public virtual ICollection<BillModel> billModels { get; set; }
        // public IEnumerable<BillModel> billModels { get; set; }

        [JsonIgnore]
        public virtual ICollection<VoucherModel> voucherModels { get; set; }
    }
}