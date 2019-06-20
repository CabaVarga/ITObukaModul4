using Homework.Models;
using Homework.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Homework.Services
{

    public class UsersService : IUsersService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
                int lineNum = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    lineNum++;

                    data = line.Split(',');

                    string name = data[0];
                    string email = data[1];

                    if (EmailExists(email))
                    {
                        // User will not be created - log the line number in the input file
                        logger.Error(String.Format("Creating user at line {0} failed. Reason: duplicate email address.", lineNum));
                        continue;
                    }

                    User createdUser = new User() { Name = name, Email = email };
                    users.Add(createdUser);
                    db.UsersRepository.Insert(createdUser);
                }
            }

            db.Save();

            return users;
        }

        #region Zadatak 1.3
        private bool EmailExists(string email)
        {
             bool count = db
                .UsersRepository.Get(filter: u => u.Email == email)
                .Count() != 0;
            return count;
        }
        #endregion

        
    }
}