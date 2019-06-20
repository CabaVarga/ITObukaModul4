using Projekat.Models;
using Projekat.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Services
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
            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public User UpdateUser(int id, string firstName, string lastName, 
            string email, string city)
        {
            User user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                user.first_name = firstName;
                user.last_name = lastName;
                user.email = email;
                // TODO Add update acceptance logic

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
    }
}