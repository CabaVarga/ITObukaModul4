using OrdersDemo.DataTransfer.Dtos.Users;
using OrdersDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(User user);
        User UpdateUser(int id, string firstName, string lastName, string userName, string email);
        User DeleteUser(int id);
        User UpdatePassword(int id, string oldPassword, string newPassword);
        User UpdateUserRole(int id, EUserRoles newRole);
        User GetByUsername(string userName);

        // PPA

        IEnumerable<PublicUserDto> GetAllUsersPublic();
        IEnumerable<PrivateUserDto> GetAllUsersPrivate();
        IEnumerable<AdminUserDto> GetAllUsersAdmin();
    }
}
