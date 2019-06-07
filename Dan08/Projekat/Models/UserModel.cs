using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class UserModel
    {
        public enum UserRoles { ROLE_CUSTOMER, ROLE_ADMIN, ROLE_SELLE };

        public int id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public UserRoles user_role { get; set; }               
    }
}