using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AppDbContext _context;

        public CatalogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Catalog>> GetAllAsync()
        {
            return await _context.Catalogs
                .Include(c => c.BookCatalogs)
                    .ThenInclude(bc => bc.Book)
                .ToListAsync();
        }

        public async Task<Catalog> GetByIdAsync(int id)
        {
            return await _context.Catalogs
                .Include(c => c.BookCatalogs)
                    .ThenInclude(bc => bc.Book)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Catalog catalog)
        {
            await _context.Catalogs.AddAsync(catalog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Catalog catalog)
        {
            _context.Catalogs.Update(catalog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog != null)
            {
                _context.Catalogs.Remove(catalog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
