using System.Collections.Generic;
using ServisniSlojBezServisa.Models;

namespace ServisniSlojBezServisa.Services
{

    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(int id);

        User CreateUser(User user);

        User UpdateUser(int id, string name, string email);

        User DeleteUser(int id);

        // Added functionality
        IEnumerable<User> GetUsersByName(string name);
    }

}