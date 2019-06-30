using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models.DTOs.User
{
    public class PrivateUserDTO : PublicUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}