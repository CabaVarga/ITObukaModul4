using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models.DTOs.Account
{
    public class PrivateAccountDTO : PublicAccountDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public AccountProvider Provider { get; set; }
    }
}