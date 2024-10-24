using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // GET ALL CARTS
        public async Task<IEnumerable<CartDTO>> GetAllCartsAsync()
        {
            var carts = await _cartRepository.GetAllAsync();

            var cartDtos = carts.Select(c => new CartDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                TotalPrice = c.TotalPrice,
                Status = c.Status,
                CreatedDate = c.CreatedDate,
                User = c.User != null ? new UserDTO
                {
                    Id = c.User.Id,
                    Username = c.User.Username,
                    Email = c.User.Email,
                    Phone = c.User.Phone,
                    Address = c.User.Address
                } : null,
                CartDetails = c.CartDetails?.Select(cd => new CartDetailDTO
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
                    } : null
                }).ToList()
            });

            return cartDtos;
        }

        // GET CART BY ID
        public async Task<CartDTO> GetCartByIdAsync(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);

            if (cart == null)
                return null;

            var cartDto = new CartDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                Status = cart.Status,
                CreatedDate = cart.CreatedDate,
                User = cart.User != null ? new UserDTO
                {
                    Id = cart.User.Id,
                    Username = cart.User.Username,
                    Email = cart.User.Email,
                    Phone = cart.User.Phone,
                    Address = cart.User.Address
                } : null,
                CartDetails = cart.CartDetails?.Select(cd => new CartDetailDTO
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
                    } : null
                }).ToList()
            };

            return cartDto;
        }

        // ADD NEW CART
        public async Task AddCartAsync(CartDTO cartDto)
        {
            var cart = new Cart
            {
                UserId = cartDto.UserId,
                TotalPrice = cartDto.TotalPrice,
                Status = cartDto.Status,
                CreatedDate = cartDto.CreatedDate,
                // Map CartDetails if necessary
                CartDetails = cartDto.CartDetails?.Select(cd => new CartDetail
                {
                    BookId = cd.BookId,
                    Price = cd.Price,
                    Quantity = cd.Quantity
                }).ToList()
            };

            await _cartRepository.AddAsync(cart);
        }

        // UPDATE EXISTING CART
        public async Task UpdateCartAsync(CartDTO cartDto)
        {
            var cart = await _cartRepository.GetByIdAsync(cartDto.Id);

            if (cart != null)
            {
                cart.UserId = cartDto.UserId;
                cart.TotalPrice = cartDto.TotalPrice;
                cart.Status = cartDto.Status;
                cart.CreatedDate = cartDto.CreatedDate;
                // Update CartDetails if necessary

                await _cartRepository.UpdateAsync(cart);
            }
        }

        // DELETE CART
        public async Task DeleteCartAsync(int id)
        {
            await _cartRepository.DeleteAsync(id);
        }
    }
}
