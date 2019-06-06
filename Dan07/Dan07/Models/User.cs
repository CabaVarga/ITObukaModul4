using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dan07.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? AddressID { get; set; }
        
        public virtual Address Address { get; set; }
    }
}