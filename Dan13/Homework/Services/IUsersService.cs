using Homework.Models;
using Homework.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public interface IUsersService
    {
        #region CRUD
        IEnumerable<User> GetAllUsers();

        User GetUser(int id);

        User CreateUser(User user);

        User UpdateUser(int id, User user);

        User DeleteUser(int id);
        #endregion

        User CreateUser(RegisterUserDTO user);
    }
}