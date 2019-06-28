using Project_3rd_clean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(User user);
        User UpdateUser(int id, string firstName, string lastName, string userName, string email);
        User DeleteUser(int id);
        User UpdatePassword(int id, string oldPassword, string newPassword);
        User UpdateUserRole(int id, User.UserRoles newRole);
        User GetByUsername(string userName);
    }
}