using Homework.Models;
using Homework.Models.DTOs.User;
using Homework.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Homework.Services
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
            IEnumerable<User> users =  db.UsersRepository.Get(includeProperties: "Address,Accounts");
            // IEnumerable<User> users = db.UsersRepository.Get();

            foreach (var u in users)
            {
                Debug.WriteLine(u.Address.City);
            }

            return users;
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(RegisterUserDTO user)
        {
            // check name
            if (db.UsersRepository.Get(filter: u => u.Name == user.Name).FirstOrDefault() != null)
            {
                throw new Exception("Name has to be unique");
            }

            // check email
            if (db.UsersRepository.Get(filter: u => u.Email == user.Email).FirstOrDefault() != null)
            {
                throw new Exception("Email already exists.");
            }

            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = "newuserpassword",
                DateOfBirth = DateTime.UtcNow
            };

            db.UsersRepository.Insert(newUser);
            db.Save();

            // email notification with unique link and or token for confirmation sent here...
            // in real scenario a pending status would be attached to the account / user...

            return newUser;
        }
    }
}