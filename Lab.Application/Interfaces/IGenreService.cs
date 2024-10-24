using Lab.Application.DTOs;

namespace Lab.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO> GetGenreByIdAsync(int id);
        Task AddGenreAsync(GenreDTO genreDto);
        Task UpdateGenreAsync(GenreDTO genreDto);
        Task DeleteGenreAsync(int id);
    }
}
