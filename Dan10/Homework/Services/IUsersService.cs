using Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(int id);

        User CreateUser(User user);

        User UpdateUser(int id, string name, string email, string city);

        User DeleteUser(int id);

        IEnumerable<User> CreateNewUsersFromFile(string path);

        // FileResource GetFileResourceForUser(int id);
    }
}