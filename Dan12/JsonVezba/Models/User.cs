using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonVezba.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public Address Address { get; set; }

        [JsonIgnore]
        public EAccesType AccesType { get; set; }

        public bool ShouldSerializeEmail()
        {
            return AccesType == EAccesType.Admin; 
        }

        public bool ShouldSerializeDateOfBirth()
        {
            return AccesType == EAccesType.Admin;
        }

        public bool ShouldSerializeAddress()
        {
            return AccesType == EAccesType.Public;
        }
    }
}