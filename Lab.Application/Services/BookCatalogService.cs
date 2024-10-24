using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class BookCatalogService : IBookCatalogService
    {
        private readonly IBookCatalogRepository _bookCatalogRepository;

        public BookCatalogService(IBookCatalogRepository bookCatalogRepository)
        {
            _bookCatalogRepository = bookCatalogRepository;
        }

        // GET ALL BOOKCATALOGS
        public async Task<IEnumerable<BookCatalogDTO>> GetAllAsync()
        {
            var bookCatalogs = await _bookCatalogRepository.GetAllAsync();

            var bookCatalogDtos = bookCatalogs.Select(bc => new BookCatalogDTO
            {
                BookId = bc.BookId,
                CatalogId = bc.CatalogId,
                Book = bc.Book != null ? new BookDTO
                {
                    Id = bc.Book.Id,
                    Title = bc.Book.Title,
                    Price = bc.Book.Price,
                    GenreName = bc.Book.Genre?.Name,
                    CatalogTitles = bc.Book.BookCatalogs?.Select(bc2 => bc2.Catalog.Title).ToList()
                } : null,
                Catalog = bc.Catalog != null ? new CatalogDTO
                {
                    Id = bc.Catalog.Id,
                    Title = bc.Catalog.Title,
                    Description = bc.Catalog.Description,
                    IsActive = bc.Catalog.IsActive
                } : null
            });

            return bookCatalogDtos;
        }

        // GET BOOKCATALOG BY ID
        public async Task<BookCatalogDTO> GetByIdAsync(int bookId, int catalogId)
        {
            var bc = await _bookCatalogRepository.GetByIdAsync(bookId, catalogId);

            if (bc == null)
                return null;

            var bookCatalogDto = new BookCatalogDTO
            {
                BookId = bc.BookId,
                CatalogId = bc.CatalogId,
                Book = bc.Book != null ? new BookDTO
                {
                    Id = bc.Book.Id,
                    Title = bc.Book.Title,
                    Price = bc.Book.Price,
                    GenreName = bc.Book.Genre?.Name,
                    CatalogTitles = bc.Book.BookCatalogs?.Select(bc2 => bc2.Catalog.Title).ToList()
                } : null,
                Catalog = bc.Catalog != null ? new CatalogDTO
                {
                    Id = bc.Catalog.Id,
                    Title = bc.Catalog.Title,
                    Description = bc.Catalog.Description,
                    IsActive = bc.Catalog.IsActive
                } : null
            };

            return bookCatalogDto;
        }

        // ADD NEW BOOKCATALOG
        public async Task AddAsync(BookCatalogDTO bookCatalogDto)
        {
            var bookCatalog = new BookCatalog
            {
                BookId = bookCatalogDto.BookId,
                CatalogId = bookCatalogDto.CatalogId
            };

            await _bookCatalogRepository.AddAsync(bookCatalog);
        }

        // DELETE BOOKCATALOG
        public async Task DeleteAsync(int bookId, int catalogId)
        {
            await _bookCatalogRepository.DeleteAsync(bookId, catalogId);
        }
    }
}
