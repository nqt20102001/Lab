using Lab.Application.DTOs;

namespace Lab.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task AddBookAsync(BookDTO bookDto);
        Task UpdateBookAsync(BookDTO bookDto);
        Task DeleteBookAsync(int id);
    }
}
