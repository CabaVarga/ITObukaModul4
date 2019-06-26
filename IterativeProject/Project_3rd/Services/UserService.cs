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
    }
}