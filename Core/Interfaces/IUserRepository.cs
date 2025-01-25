
using Core.Queries;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<(List<User>, int)> GetAllAsync(UserQuery userQuery);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(Guid id, User user);
        Task<User?> DeleteAsync(Guid id);
    }
}