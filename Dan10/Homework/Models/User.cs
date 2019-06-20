using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Homework.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        // Configuration through Fluent API, but only in EF Core
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal Expenses {
            get
            {
                switch (this.City)
                {
                    case "Beograd":
                        return 10000;
                    case "Novi Sad":
                        return 5000;
                    default:
                        return 0;
                }
            }
            private set { }
        }
    }
}