using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class CategoryModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string category_name { get; set; }

        [Required]
        [StringLength(200)]
        public string category_description { get; set; }

        // Navigation properties
        [JsonIgnore]
        public ICollection<OfferModel> offerModels { get; set; }
    }
}