using Projekat.Models;

namespace Projekat.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<BillModel> BillsRepository { get; }
        IGenericRepository<CategoryModel> CategoriesRepository { get; }
        IGenericRepository<OfferModel> OffersRepository { get; }
        IGenericRepository<UserModel> UsersRepository { get; }
        IGenericRepository<VoucherModel> VouchersRepository { get; }


        void Save();
    }
}