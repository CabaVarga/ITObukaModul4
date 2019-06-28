using Project_3rd_clean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UsersRepository { get; }

        IGenericRepository<Category> CategoriesRepository { get; }

        IGenericRepository<Offer> OffersRepository { get; }

        IGenericRepository<Bill> BillsRepository { get; }

        IGenericRepository<Voucher> VouchersRepository { get; }

        void Save();
    }
}