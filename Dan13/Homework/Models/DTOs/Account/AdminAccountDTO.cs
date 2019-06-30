using Homework.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models.DTOs.Account
{
    public class AdminAccountDTO : PrivateAccountDTO
    {
        public string Link { get; set; }

        public AdminUserDTO Owner { get; set; }
    }
}