using Lab.Domain.Entities;

namespace Lab.Domain.Interfaces
{
    public interface IBookCatalogRepository
    {
        Task<IEnumerable<BookCatalog>> GetAllAsync();
        Task<BookCatalog> GetByIdAsync(int bookId, int catalogId);
        Task AddAsync(BookCatalog bookCatalog);
        Task DeleteAsync(int bookId, int catalogId);
    }
}
