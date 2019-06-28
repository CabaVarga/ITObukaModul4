using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Homework.Models
{
    public class User
    {
        public User()
        {
            this.SocialAccounts = new HashSet<SocialAccount>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public virtual Address Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<SocialAccount> SocialAccounts { get; set; }
    }
}