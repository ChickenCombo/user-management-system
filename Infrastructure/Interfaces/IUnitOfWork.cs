using Core.Interfaces;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task<int> SaveChangesAsync();
    }
}