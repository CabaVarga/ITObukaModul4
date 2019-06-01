using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace radproj_v1.Models
{
    [Table("radnik")]
    public class Radnik
    {
        [Column("mbr")]
        [Key]
        public long MaticniBroj { get; set; }
        [Column("ime")]
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public long SefMaticniBroj { get; set; }
        public decimal Plata { get; set; }
        public decimal Premija { get; set; }
        public DateTime GodinaRodjenja { get; set; }

        [ForeignKey("MaticniBroj")]
        public Radnik SefRadnika { get; set; } 
    }
}