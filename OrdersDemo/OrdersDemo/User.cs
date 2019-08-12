using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.Domain
{
    public class User
    {
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

        // ZADATAK 1.2 Iskljuciti korisnicku lozinku iz procesa serijalizacije 
        [JsonIgnore]
        [Required]
        [StringLength(45)]
        public string password { get; set; }

        [Required]
        [StringLength(45)]
        public string email { get; set; }

        public EUserRoles user_role { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<Offer> offerModels { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bill> billModels { get; set; }
        // public IEnumerable<BillModel> billModels { get; set; }

        [JsonIgnore]
        public virtual ICollection<Voucher> voucherModels { get; set; }

        [JsonIgnore]
        public EAccessType AccessType { get; set; }

        public bool ShouldSerializeEmail()
        {
            return AccessType == EAccessType.ADMIN;
        }
    }
}
