using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Homework.Models
{
    public class Address
    {
        public Address()
        {
            Users = new List<User>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}