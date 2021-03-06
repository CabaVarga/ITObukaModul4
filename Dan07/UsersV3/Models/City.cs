﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UsersV3.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}