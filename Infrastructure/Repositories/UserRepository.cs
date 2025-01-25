using Core.Entities;
using Core.Interfaces;
using Core.Queries;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<(List<User>, int)> GetAllAsync(UserQuery userQuery)
        {
            var users = _context.User.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userQuery.Role))
            {
                users = users.Where(u => u.Role == userQuery.Role);
            }

            var totalCount = await users.CountAsync();
            var skipNumber = (userQuery.PageNumber - 1) * userQuery.PageSize;
            var queriedItems = await users.Skip(skipNumber).Take(userQuery.PageSize).ToListAsync();

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
            await _context.SaveChangesAsync();

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
            existingUser.Role = user.Role;

            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
