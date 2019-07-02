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

            //foreach (var u in users)
            //{
            //    Debug.WriteLine(u.Address.City);
            //}

            return users;
        }

        public User GetUser(int id)
        {
            return db.UsersRepository.GetByID(id);
        }

        public User CreateUser(User user)
        {
            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public User UpdateUser(int id, User user)
        {
            User updatedUser = db.UsersRepository.GetByID(id);

            if (updatedUser != null)
            {
                updatedUser.Name = user.Name;
                updatedUser.Email = user.Email;
                updatedUser.DateOfBirth = user.DateOfBirth;
                updatedUser.Password = user.Password;
                updatedUser.Address = user.Address;

                db.UsersRepository.Update(updatedUser);
                db.Save();
            }

            return updatedUser;
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