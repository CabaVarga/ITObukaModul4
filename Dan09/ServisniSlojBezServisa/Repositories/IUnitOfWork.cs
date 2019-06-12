using ServisniSlojBezServisa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServisniSlojBezServisa.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UsersRepository { get; }

        void Save();
    }
}