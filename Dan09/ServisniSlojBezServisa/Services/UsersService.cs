using ServisniSlojBezServisa.Models;
using ServisniSlojBezServisa.Repositories;
using System.Collections.Generic;

namespace ServisniSlojBezServisa.Services
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
            // user.UserRole = UserRoles.ROLE_CUSTOMER;

            db.UsersRepository.Insert(user);
            db.Save();

            return user;
        }

        public User UpdateUser(int id, string name, string email)
        {
            User user = db.UsersRepository.GetByID(id);

            if (user != null)
            {
                user.Name = name;
                user.Email = email;

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

        // Added functionality
        public IEnumerable<User> GetUsersByName(string name)
        {
            return db.UsersRepository.Get(
                filter: u => u.Name == name);
        }
    }
}