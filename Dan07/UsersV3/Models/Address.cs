using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UsersV3.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        public virtual City City { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}