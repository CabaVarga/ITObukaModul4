using Homework.Models.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models.DTOs.User
{
    public class AdminUserDTO : PrivateUserDTO
    {
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public new IEnumerable<AdminAccountDTO> Accounts { get; set; }
    }
}