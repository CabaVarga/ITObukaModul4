using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Services
{
    interface IUsersService
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(int id);

        User CreateUser(User user);

        User UpdateUser(int id, string name, string email, string city);

        User DeleteUser(int id);
    }
}
