using Core.Entities;
using Core.Interfaces;
using Core.Queries;
using Infrastructure.QueryBuilders;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<(List<User>, int)> GetAllAsync(UserQuery userQuery)
        {
            var userQueryBuilder = new UserQueryBuilder(_context.User.AsQueryable());

            var filteredUsers = userQueryBuilder
                .FilterByFirstName(userQuery.FirstName)
                .FilterByMiddleName(userQuery.MiddleName)
                .FilterByLastName(userQuery.LastName)
                .FilterByRole(userQuery.Role)
                .FilterByEmail(userQuery.Email)
                .Query();

            var totalCount = await filteredUsers.CountAsync();
            var skipNumber = (userQuery.PageNumber - 1) * userQuery.PageSize;
            var queriedItems = await filteredUsers.Skip(skipNumber).Take(userQuery.PageSize).ToListAsync();

            return (queriedItems, totalCount);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.User.AddAsync(user);

            return user;
        }

        public async Task<User?> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(eu => eu.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.MiddleName = user.MiddleName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            return existingUser;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            _context.Remove(user);

            return user;
        }
    }
}
