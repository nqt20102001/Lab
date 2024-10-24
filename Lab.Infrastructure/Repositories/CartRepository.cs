using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts
                .Include(c => c.User)
                .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Book)
                .ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts
                .Include(c => c.User)
                .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Book)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
