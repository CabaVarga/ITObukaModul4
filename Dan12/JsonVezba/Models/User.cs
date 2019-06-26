using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

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
        public EAccessType AccessType { get; set; }

        public bool ShouldSerializeEmail()
        {
            return AccessType == EAccessType.Admin; 
        }

        public bool ShouldSerializeDateOfBirth()
        {
            return AccessType == EAccessType.Admin;
        }

        public bool ShouldSerializeAddress()
        {
            return AccessType == EAccessType.Public;
        }
    }
}