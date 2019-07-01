using Homework.Models;
using Homework.Models.DTOs.User;
using Homework.Repositories;
using Homework.Utilities;
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
            // TEMPORARILY TURN OFF - for testing purposes

            //// check name
            //if (db.UsersRepository.Get(filter: u => u.Name == user.Name).FirstOrDefault() != null)
            //{
            //    throw new Exception("Name has to be unique");
            //}

            //// check email
            //if (db.UsersRepository.Get(filter: u => u.Email == user.Email).FirstOrDefault() != null)
            //{
            //    throw new Exception("Email already exists.");
            //}

            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = "newuserpassword",
                DateOfBirth = DateTime.UtcNow
            };

            Debug.WriteLine(String.Format("User ID of created user: {0}", newUser.Id));
            // id is 0
            db.UsersRepository.Insert(newUser);

            Debug.WriteLine(String.Format("User ID of created user, after inserting: {0}", newUser.Id));
            // id is still 0

            db.Save();

            // email notification with unique link and or token for confirmation sent here...
            // in real scenario a pending status would be attached to the account / user...

            Debug.WriteLine(String.Format("User ID of created user, after saving: {0}", newUser.Id));
            // id is 8... so after the save operation it will autoupdate the id, nice!
            // this is btw a fine reason for sending back the full model to the controller
            // otherwise the controller would not know the id of the new resource...

            return newUser;
        }
    }
}