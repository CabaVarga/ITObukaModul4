using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUser(int id);
        UserModel CreateUser(UserModel user);
        UserModel UpdateUser(int id, string firstName, string lastName, string userName, string email);
        UserModel DeleteUser(int id);
        UserModel UpdatePassword(int id, string oldPassword, string newPassword);
        UserModel UpdateUserRole(int id, UserModel.UserRoles newRole);
        UserModel GetByUsername(string userName);
    }
}