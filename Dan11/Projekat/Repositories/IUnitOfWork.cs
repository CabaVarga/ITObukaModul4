using Projekat.Models;

namespace Projekat.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Bill> BillsRepository { get; }
        IGenericRepository<Category> CategoriesRepository { get; }
        IGenericRepository<Offer> OffersRepository { get; }
        IGenericRepository<User> UsersRepository { get; }
        IGenericRepository<Voucher> VouchersRepository { get; }


        void Save();
    }
}