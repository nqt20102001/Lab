using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;

        public CartDetailService(ICartDetailRepository cartDetailRepository)
        {
            _cartDetailRepository = cartDetailRepository;
        }

        // GET ALL CART DETAILS
        public async Task<IEnumerable<CartDetailDTO>> GetAllCartDetailsAsync()
        {
            var cartDetails = await _cartDetailRepository.GetAllAsync();

            var cartDetailDtos = cartDetails.Select(cd => new CartDetailDTO
            {
                Id = cd.Id,
                CartId = cd.CartId,
                BookId = cd.BookId,
                Price = cd.Price,
                Quantity = cd.Quantity,
                Book = cd.Book != null ? new BookDTO
                {
                    Id = cd.Book.Id,
                    Title = cd.Book.Title,
                    Price = cd.Book.Price,
                    GenreName = cd.Book.Genre?.Name,
                    CatalogTitles = cd.Book.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
                } : null,
                Cart = cd.Cart != null ? new CartDTO
                {
                    Id = cd.Cart.Id,
                    TotalPrice = cd.Cart.TotalPrice,
                    Status = cd.Cart.Status,
                    CreatedDate = cd.Cart.CreatedDate,
                    UserId = cd.Cart.UserId
                } : null
            });

            return cartDetailDtos;
        }

        // GET CART DETAIL BY ID
        public async Task<CartDetailDTO> GetCartDetailByIdAsync(int id)
        {
            var cd = await _cartDetailRepository.GetByIdAsync(id);

            if (cd == null)
                return null;

            var cartDetailDto = new CartDetailDTO
            {
                Id = cd.Id,
                CartId = cd.CartId,
                BookId = cd.BookId,
                Price = cd.Price,
                Quantity = cd.Quantity,
                Book = cd.Book != null ? new BookDTO
                {
                    Id = cd.Book.Id,
                    Title = cd.Book.Title,
                    Price = cd.Book.Price,
                    GenreName = cd.Book.Genre?.Name,
                    CatalogTitles = cd.Book.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
                } : null,
                Cart = cd.Cart != null ? new CartDTO
                {
                    Id = cd.Cart.Id,
                    TotalPrice = cd.Cart.TotalPrice,
                    Status = cd.Cart.Status,
                    CreatedDate = cd.Cart.CreatedDate,
                    UserId = cd.Cart.UserId
                } : null
            };

            return cartDetailDto;
        }

        // ADD NEW CART DETAIL
        public async Task AddCartDetailAsync(CartDetailDTO cartDetailDto)
        {
            var cartDetail = new CartDetail
            {
                CartId = cartDetailDto.CartId,
                BookId = cartDetailDto.BookId,
                Price = cartDetailDto.Price,
                Quantity = cartDetailDto.Quantity
            };

            await _cartDetailRepository.AddAsync(cartDetail);
        }

        // UPDATE EXISTING CART DETAIL
        public async Task UpdateCartDetailAsync(CartDetailDTO cartDetailDto)
        {
            var cartDetail = await _cartDetailRepository.GetByIdAsync(cartDetailDto.Id);

            if (cartDetail != null)
            {
                cartDetail.CartId = cartDetailDto.CartId;
                cartDetail.BookId = cartDetailDto.BookId;
                cartDetail.Price = cartDetailDto.Price;
                cartDetail.Quantity = cartDetailDto.Quantity;

                await _cartDetailRepository.UpdateAsync(cartDetail);
            }
        }

        // DELETE CART DETAIL
        public async Task DeleteCartDetailAsync(int id)
        {
            await _cartDetailRepository.DeleteAsync(id);
        }
    }
}
