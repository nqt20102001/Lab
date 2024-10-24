using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                // Include navigation properties
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartDetails)
                        .ThenInclude(cd => cd.Book)
                            .ThenInclude(b => b.Genre)
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartDetails)
                        .ThenInclude(cd => cd.Book)
                            .ThenInclude(b => b.BookCatalogs)
                                .ThenInclude(bc => bc.Catalog)
                .ToListAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartDetails)
                        .ThenInclude(cd => cd.Book)
                            .ThenInclude(b => b.Genre)
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartDetails)
                        .ThenInclude(cd => cd.Book)
                            .ThenInclude(b => b.BookCatalogs)
                                .ThenInclude(bc => bc.Catalog)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Carts)
                    .ThenInclude(c => c.CartDetails)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
