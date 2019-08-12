using OrdersDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Users
{
    public class AdminUserDto : PrivateUserDto
    {
        public EUserRoles UserRole { get; set; }
    }
}
