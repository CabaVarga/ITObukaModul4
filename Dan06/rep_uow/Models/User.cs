using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rep_uow.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Updating the model for Task 2.1
        [Column(TypeName="DATE")]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(13)]
        [Column(TypeName ="VARCHAR")]
        public string Phone { get; set; }

        [MaxLength(13)]
        [Column(TypeName ="CHAR")]
        public string JMBG { get; set; }

        [MaxLength(9)]
        [Column(TypeName ="CHAR")]
        public string IdentityCardNumber { get; set; }
    }
}