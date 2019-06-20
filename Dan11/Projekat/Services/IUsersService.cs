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
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetUser(int id);

        UserModel CreateUser(UserModel user);

        UserModel UpdateUser(int id, string name, string email, string city);

        UserModel DeleteUser(int id);
    }
}
