using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class BookCatalogRepository : IBookCatalogRepository
    {
        private readonly AppDbContext _context;

        public BookCatalogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookCatalog>> GetAllAsync()
        {
            return await _context.BookCatalogs
                .Include(bc => bc.Book)
                .Include(bc => bc.Catalog)
                .ToListAsync();
        }

        public async Task<BookCatalog> GetByIdAsync(int bookId, int catalogId)
        {
            return await _context.BookCatalogs
                .Include(bc => bc.Book)
                .Include(bc => bc.Catalog)
                .FirstOrDefaultAsync(bc => bc.BookId == bookId && bc.CatalogId == catalogId);
        }

        public async Task AddAsync(BookCatalog bookCatalog)
        {
            await _context.BookCatalogs.AddAsync(bookCatalog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int bookId, int catalogId)
        {
            var bookCatalog = await _context.BookCatalogs.FindAsync(bookId, catalogId);
            if (bookCatalog != null)
            {
                _context.BookCatalogs.Remove(bookCatalog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
