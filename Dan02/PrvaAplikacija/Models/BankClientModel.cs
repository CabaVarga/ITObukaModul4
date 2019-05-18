using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrvaAplikacija.Models
{
    public class BankClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public char Bonitet { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Grad { get; set; }

        public BankClientModel() : this(1, "Ime", "Prezime", "Email") { }
        public BankClientModel(int id, string name, string surname, string email)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
        }

        public BankClientModel(int id, string name, string surname, string email, DateTime datumRodjenja)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            DatumRodjenja = datumRodjenja;
        }

        public BankClientModel(int id, string name, string surname, string email, DateTime datumRodjenja, string grad)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            DatumRodjenja = datumRodjenja;
            Grad = grad;
        }
    }
}