﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Homework.Models
{
    public class Account
    {
        public int Id { get; set; }
        
        public string Link { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountProvider Provider { get; set; }

        [ForeignKey("Owner")]
        public int? UserId { get; set; }

        public virtual User Owner { get; set; }
    }
}