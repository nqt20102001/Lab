using Lab.Application.DTOs;

namespace Lab.Application.Interfaces
{
    public interface ICartDetailService
    {
        Task<IEnumerable<CartDetailDTO>> GetAllCartDetailsAsync();
        Task<CartDetailDTO> GetCartDetailByIdAsync(int id);
        Task AddCartDetailAsync(CartDetailDTO cartDetailDto);
        Task UpdateCartDetailAsync(CartDetailDTO cartDetailDto);
        Task DeleteCartDetailAsync(int id);
    }
}
