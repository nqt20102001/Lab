using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        // GET ALL GENRES
        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();

            var genreDtos = genres.Select(g => new GenreDTO
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                IsActive = g.IsActive,
                Books = g.Books?.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    CreatedDate = b.CreatedDate,
                    Available = b.Available,
                    GenreName = b.Genre?.Name,
                    CatalogTitles = b.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
                }).ToList()
            });

            return genreDtos;
        }

        // GET GENRE BY ID
        public async Task<GenreDTO> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
                return null;

            var genreDto = new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
                IsActive = genre.IsActive,
                Books = genre.Books?.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    CreatedDate = b.CreatedDate,
                    Available = b.Available,
                    GenreName = b.Genre?.Name,
                    CatalogTitles = b.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
                }).ToList()
            };

            return genreDto;
        }

        // ADD NEW GENRE
        public async Task AddGenreAsync(GenreDTO genreDto)
        {
            var genre = new Genre
            {
                Name = genreDto.Name,
                Description = genreDto.Description,
                IsActive = genreDto.IsActive
                // Nếu cần thêm sách, bạn có thể xử lý ở đây
            };

            await _genreRepository.AddAsync(genre);
        }

        // UPDATE EXISTING GENRE
        public async Task UpdateGenreAsync(GenreDTO genreDto)
        {
            var genre = await _genreRepository.GetByIdAsync(genreDto.Id);

            if (genre != null)
            {
                genre.Name = genreDto.Name;
                genre.Description = genreDto.Description;
                genre.IsActive = genreDto.IsActive;
                // Cập nhật danh sách sách nếu cần

                await _genreRepository.UpdateAsync(genre);
            }
        }

        // DELETE GENRE
        public async Task DeleteGenreAsync(int id)
        {
            await _genreRepository.DeleteAsync(id);
        }
    }
}
