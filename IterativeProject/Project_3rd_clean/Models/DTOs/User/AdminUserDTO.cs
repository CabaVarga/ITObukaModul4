using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Project_3rd_clean.Models.User;

namespace Project_3rd_clean.Models.DTOs.User
{
    public class AdminUserDTO : PrivateUserDTO
    {
        public UserRoles UserRole { get; set; }
    }
}