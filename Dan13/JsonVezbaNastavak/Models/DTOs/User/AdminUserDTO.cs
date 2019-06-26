using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JsonVezbaNastavak.Models.DTOs.User
{
    public class AdminUserDTO : PrivateUserDTO
    {
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}