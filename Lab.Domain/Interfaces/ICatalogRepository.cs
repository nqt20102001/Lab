using Lab.Domain.Entities;

namespace Lab.Domain.Interfaces
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Catalog>> GetAllAsync();
        Task<Catalog> GetByIdAsync(int id);
        Task AddAsync(Catalog catalog);
        Task UpdateAsync(Catalog catalog);
        Task DeleteAsync(int id);
    }
}
