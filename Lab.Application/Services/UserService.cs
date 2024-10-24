using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Constructor without IMapper
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET ALL USERS
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Manual mapping from User to UserDTO
            var userDtos = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                // Optionally map Carts if needed
                Carts = u.Carts?.Select(c => new CartDTO
                {
                    Id = c.Id,
                    TotalPrice = c.TotalPrice,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate,
                    CartDetails = c.CartDetails?.Select(cd => new CartDetailDTO
                    {
                        Id = cd.Id,
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
                }).ToList()
            });

            return userDtos;
        }

        // GET USER BY ID
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            // Manual mapping from User to UserDTO
            var userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                // Optionally map Carts if needed
                Carts = user.Carts?.Select(c => new CartDTO
                {
                    Id = c.Id,
                    TotalPrice = c.TotalPrice,
                    Status = c.Status,
                    CreatedDate = c.CreatedDate,
                    CartDetails = c.CartDetails?.Select(cd => new CartDetailDTO
                    {
                        Id = cd.Id,
                        Price = cd.Price,
                        Quantity = cd.Quantity,
                        Book = new BookDTO
                        {
                            Id = cd.Book.Id,
                            Title = cd.Book.Title,
                            Price = cd.Book.Price,
                            GenreName = cd.Book.Genre?.Name,
                            CatalogTitles = cd.Book.BookCatalogs?.Select(bc => bc.Catalog.Title).ToList()
                        }
                    }).ToList()
                }).ToList()
            };

            return userDto;
        }

        // ADD NEW USER
        public async Task AddUserAsync(UserDTO userDto)
        {
            // Manual mapping from UserDTO to User
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address,
                // If you need to add Carts, map them here
                // Carts = userDto.Carts?.Select(c => new Cart { ... }).ToList()
            };

            await _userRepository.AddAsync(user);
        }

        // UPDATE EXISTING USER
        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);

            if (user != null)
            {
                user.Username = userDto.Username;
                user.Email = userDto.Email;
                user.Phone = userDto.Phone;
                user.Address = userDto.Address;
                // Update other properties if necessary

                await _userRepository.UpdateAsync(user);
            }
        }

        // DELETE USER
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
