using OrdersDemo.Domain;
using OrdersDemo.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.Repository.UnitOfWork
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
