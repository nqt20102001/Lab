using Lab.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Application.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDTO>> GetAllCartsAsync();
        Task<CartDTO> GetCartByIdAsync(int id);
        Task AddCartAsync(CartDTO cartDto);
        Task UpdateCartAsync(CartDTO cartDto);
        Task DeleteCartAsync(int id);
    }
}
