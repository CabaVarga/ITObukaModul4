using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<UserModel> UsersRepository { get; }

        IGenericRepository<CategoryModel> CategoriesRepository { get; }

        IGenericRepository<OfferModel> OffersRepository { get; }

        IGenericRepository<BillModel> BillsRepository { get; }

        IGenericRepository<VoucherModel> VouchersRepository { get; }

        void Save();
    }
}