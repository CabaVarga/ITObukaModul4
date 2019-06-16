using Homework.Models;
using Homework.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
            return db.UsersRepository.Get();
        }

        public User GetUser(int id)
        {
            return db.UsersRepository.GetByID(id);
        }

        public User CreateUser(User user)
        {
            if (EmailExists(user.Email))
            {
                return null;
            }

            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public User UpdateUser(int id, string name, string email, string city)
        {
            if (EmailExists(email))
            {
                return null;
            }

            User user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                user.City = city;

                db.UsersRepository.Update(user);
                db.Save();
            }

            return user;
        }

        public User DeleteUser(int id)
        {
            User user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                db.UsersRepository.Delete(user);
                db.Save();
            }

            return user;
        }

        public IEnumerable<User> CreateNewUsersFromFile(string path)
        {
            List<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                string[] data;

                while ((line = sr.ReadLine()) != null)
                {
                    data = line.Split(',');
                    users.Add(new User() { Name = data[0], Email = data[1] });
                }
            }

            if (users.Count != 0)
            {
                BulkCreateUsers(users);
            }

            return users;
        }

        private void BulkCreateUsers(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                db.UsersRepository.Insert(user);
            }

            db.Save();
        }

        private bool EmailExists(string email)
        {
            return db.UsersRepository.Get(
                        filter: u => u.Email == email)
                    .Count() == 0;  
        }
    }
}