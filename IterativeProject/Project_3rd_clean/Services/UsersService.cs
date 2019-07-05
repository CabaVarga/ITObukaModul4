using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.User;
using Project_3rd_clean.Repositories;

namespace Project_3rd_clean.Services
{
    public class UsersService : IUsersService
    {
        private IUnitOfWork db;

        public UsersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return db.UsersRepository.Get();
        }

        public User GetUser(int id)
        {
            return db.UsersRepository.GetByID(id);
        }


        public User CreateUser(User user)
        {
            user.user_role = User.UserRoles.ROLE_CUSTOMER;

            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public User UpdateUser(int id, string firstName, string lastName, string userName, string email)
        {
            User user = db.UsersRepository.GetByID(id);

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

        public User DeleteUser(int id)
        {
            User userToDelete = db.UsersRepository.GetByID(id);

            if (userToDelete != null)
            {
                db.UsersRepository.Delete(userToDelete);
                db.Save();
            }

            return userToDelete;
        }

        public User UpdatePassword(int id, string oldPassword, string newPassword)
        {
            User user = db.UsersRepository.GetByID(id);

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

        public User UpdateUserRole(int id, User.UserRoles newRole)
        {
            User user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                user.user_role = newRole;
                db.UsersRepository.Update(user);
                db.Save();
            }

            return user;
        }

        public User GetByUsername(string userName)
        {
            return db
                .UsersRepository.Get(filter: u => u.username == userName)
                .FirstOrDefault();
        }

        public IEnumerable<PublicUserDTO> GetAllUsersPublic()
        {
            return db.UsersRepository.Get()
                .Select(u =>
                {
                    return new PublicUserDTO()
                    {
                        Id = u.id,
                        UserName = u.username
                    };
                });
        }

        public IEnumerable<PrivateUserDTO> GetAllUsersPrivate()
        {
            return db.UsersRepository.Get()
                .Select(u =>
                {
                    return new PrivateUserDTO()
                    {
                        Id = u.id,
                        UserName = u.username,
                        FirstName = u.first_name,
                        LastName = u.last_name
                    };
                });
        }

        public IEnumerable<AdminUserDTO> GetAllUsersAdmin()
        {
            return db.UsersRepository.Get()
                .Select(u =>
                {
                    return new AdminUserDTO()
                    {
                        Id = u.id,
                        UserName = u.username,
                        FirstName = u.first_name,
                        LastName = u.last_name,
                        UserRole = u.user_role
                    };
                });
        }
    }
}