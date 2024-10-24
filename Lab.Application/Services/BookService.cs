using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            // Mapping từ Book sang BookDTO
            var bookDtos = books.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                GenreName = b.Genre?.Name,
                CatalogTitles = b.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
            });

            return bookDtos;
            //var books = await _bookRepository.GetAllAsync();
            //return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                return null;

            // Mapping sang BookDTO
            var bookDto = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                GenreName = book.Genre?.Name,
                CatalogTitles = book.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
            };

            return bookDto;
        }

        public async Task AddBookAsync(BookDTO bookDto)
        {
            // Mapping từ BookDTO sang Book
            var book = new Book
            {
                Title = bookDto.Title,
                Price = bookDto.Price,
                // Bạn cần xử lý thêm cho Genre và Catalog nếu cần
            };

            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(BookDTO bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookDto.Id);

            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Price = bookDto.Price;
                // Cập nhật các trường khác nếu cần

                await _bookRepository.UpdateAsync(book);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
