using Lab.Application.DTOs;

namespace Lab.Application.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogDTO>> GetAllCatalogsAsync();
        Task<CatalogDTO> GetCatalogByIdAsync(int id);
        Task AddCatalogAsync(CatalogDTO catalogDto);
        Task UpdateCatalogAsync(CatalogDTO catalogDto);
        Task DeleteCatalogAsync(int id);
    }
}
