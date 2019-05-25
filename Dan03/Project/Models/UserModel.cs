using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public enum UserRole { ROLE_CUSTOMER, ROLE_ADMIN, ROLE_SELLER }
    public class UserModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public UserRole userRole { get; set; }
    }
}