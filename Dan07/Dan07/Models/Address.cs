using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dan07.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}