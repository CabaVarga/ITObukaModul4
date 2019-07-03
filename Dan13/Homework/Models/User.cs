using Homework.Utilities.JSONConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [StringLength(50, ErrorMessage ="Max length is 50 characters")]
        public string Name { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        // [Required]
        // [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 255 characters long")]
        public string Password { get; set; }
        
        public virtual Address Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}