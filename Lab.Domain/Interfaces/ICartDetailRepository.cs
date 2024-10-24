using Lab.Domain.Entities;

namespace Lab.Domain.Interfaces
{
    public interface ICartDetailRepository
    {
        Task<IEnumerable<CartDetail>> GetAllAsync();
        Task<CartDetail> GetByIdAsync(int id);
        Task AddAsync(CartDetail cartDetail);
        Task UpdateAsync(CartDetail cartDetail);
        Task DeleteAsync(int id);
    }
}
