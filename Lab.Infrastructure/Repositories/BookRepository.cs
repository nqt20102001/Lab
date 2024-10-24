using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.BookCatalogs)
                    .ThenInclude(bc => bc.Catalog)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.BookCatalogs)
                    .ThenInclude(bc => bc.Catalog)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetByTitleAsync(string title)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.BookCatalogs)
                    .ThenInclude(bc => bc.Catalog)
                .FirstOrDefaultAsync(b => b.Title == title);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
