using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly AppDbContext _context;

        public CartDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartDetail>> GetAllAsync()
        {
            return await _context.CartDetails
                .Include(cd => cd.Cart)
                .Include(cd => cd.Book)
                .ToListAsync();
        }

        public async Task<CartDetail> GetByIdAsync(int id)
        {
            return await _context.CartDetails
                .Include(cd => cd.Cart)
                .Include(cd => cd.Book)
                .FirstOrDefaultAsync(cd => cd.Id == id);
        }

        public async Task AddAsync(CartDetail cartDetail)
        {
            await _context.CartDetails.AddAsync(cartDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartDetail cartDetail)
        {
            _context.CartDetails.Update(cartDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cartDetail = await _context.CartDetails.FindAsync(id);
            if (cartDetail != null)
            {
                _context.CartDetails.Remove(cartDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
