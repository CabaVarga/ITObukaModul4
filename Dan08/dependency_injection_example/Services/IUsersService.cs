using dependency_injection_example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dependency_injection_example.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetActiveUsers();
    }
}