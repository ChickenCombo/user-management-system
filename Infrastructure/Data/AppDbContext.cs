using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
        : DbContext(dbContextOptions)
    {
        public DbSet<User> User => Set<User>();
    }
}