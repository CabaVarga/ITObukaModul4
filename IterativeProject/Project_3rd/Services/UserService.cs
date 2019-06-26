using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_3rd.Models;
using Project_3rd.Repositories;

namespace Project_3rd.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork db;

        public UserService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return db.UsersRepository.Get();
        }

        public UserModel GetUser(int id)
        {
            return db.UsersRepository.GetByID(id);
        }


        public UserModel CreateUser(UserModel user)
        {
            user.user_role = UserModel.UserRoles.ROLE_CUSTOMER;

            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public UserModel UpdateUser(int id, string firstName, string lastName, string userName, string email)
        {
            UserModel user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                user.first_name = firstName;
                user.last_name = lastName;
                user.username = userName;
                user.email = email;

                db.UsersRepository.Update(user);
                db.Save();
            }

            return user;
        }

        public UserModel DeleteUser(int id)
        {
            UserModel userToDelete = db.UsersRepository.GetByID(id);

            if (userToDelete != null)
            {
                db.UsersRepository.Delete(userToDelete);
                db.Save();
            }

            return userToDelete;
        }

        public UserModel UpdatePassword(int id, string oldPassword, string newPassword)
        {
            UserModel user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                if (user.password == oldPassword)
                {
                    user.password = newPassword;
                }

                db.UsersRepository.Update(user);
                db.Save();
            }

            return user;
        }

        public UserModel UpdateUserRole(int id, UserModel.UserRoles newRole)
        {
            UserModel user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                user.user_role = newRole;
                db.UsersRepository.Update(user);
                db.Save();
            }

            return user;
        }

        public UserModel GetByUsername(string userName)
        {
            return db
                .UsersRepository.Get(filter: u => u.username == userName)
                .FirstOrDefault();
        }
    }
}