using ServisniSlojBezServisa.Models;

namespace ServisniSlojBezServisa.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UsersRepository { get; }

        void Save();
    }
}