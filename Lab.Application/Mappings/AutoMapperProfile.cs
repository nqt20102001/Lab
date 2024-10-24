using AutoMapper;
using Lab.Application.DTOs;
using Lab.Domain.Entities;

namespace Lab.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping User to UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Carts, opt => opt.MapFrom(src => src.Carts));

            // Mapping UserDTO to User
            CreateMap<UserDTO, User>();

            // Mapping Cart to CartDTO
            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.CartDetails, opt => opt.MapFrom(src => src.CartDetails));

            // Mapping CartDTO to Cart
            CreateMap<CartDTO, Cart>();

            // Mapping CartDetail to CartDetailDTO
            CreateMap<CartDetail, CartDetailDTO>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

            // Mapping CartDetailDTO to CartDetail
            CreateMap<CartDetailDTO, CartDetail>();

            // Mapping Book to BookDTO (đã tạo ở bước trước)
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.CatalogTitles, opt => opt.MapFrom(src => src.BookCatalogs.Select(bc => bc.Catalog.Title)));

            // Mapping BookDTO to Book
            CreateMap<BookDTO, Book>();
        }
    }
}
