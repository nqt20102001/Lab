using Lab.Domain.Entities;
using Lab.Domain.Interfaces;
using Lab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres
                .Include(g => g.Books)
                    .ThenInclude(b => b.BookCatalogs)
                        .ThenInclude(bc => bc.Catalog)
                .ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Books)
                    .ThenInclude(b => b.BookCatalogs)
                        .ThenInclude(bc => bc.Catalog)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}
