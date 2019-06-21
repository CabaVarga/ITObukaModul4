using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonVezba.Models
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
        
        [JsonIgnore]
        public EAccesType AccesType { get; set; }

        public bool ShouldSerializeId()
        {
            return AccesType == EAccesType.Public;
        }

        public bool ShouldSerializeStreet()
        {
            return AccesType == EAccesType.Public;
        }

        public bool ShouldSerializeCity()
        {
            return AccesType == EAccesType.Public;
        }

        public bool ShouldSerializeCountry()
        {
            return AccesType == EAccesType.Public;
        }

        public bool ShouldSerializeUsers()
        {
            return AccesType == EAccesType.Public;
        }
    }
}