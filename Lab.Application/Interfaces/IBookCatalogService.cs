using Lab.Application.DTOs;

namespace Lab.Application.Interfaces
{
    public interface IBookCatalogService
    {
        Task<IEnumerable<BookCatalogDTO>> GetAllAsync();
        Task<BookCatalogDTO> GetByIdAsync(int bookId, int catalogId);
        Task AddAsync(BookCatalogDTO bookCatalogDto);
        Task DeleteAsync(int bookId, int catalogId);
    }
}
