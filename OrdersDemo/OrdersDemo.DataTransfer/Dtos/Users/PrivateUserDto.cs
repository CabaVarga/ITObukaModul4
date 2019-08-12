using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Users
{
    public class PrivateUserDto : PublicUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
