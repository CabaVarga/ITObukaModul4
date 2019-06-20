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
            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public UserModel UpdateUser(int id, string firstName, string lastName, 
            string email, string city)
        {
            UserModel user = db.UsersRepository.GetByID(id);

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

        public UserModel DeleteUser(int id)
        {
            UserModel user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                db.UsersRepository.Delete(user);
                db.Save();
            }

            return user;
        }
    }
}