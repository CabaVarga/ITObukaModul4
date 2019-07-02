using Homework.Utilities.JSONConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Homework.Models
{
    // You need to read up on this, how it's used, especially the read part...
    // [JsonConverter(typeof(UserConverter))]
    public class User
    {
        public User()
        {
            this.Accounts = new HashSet<Account>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public virtual Address Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}