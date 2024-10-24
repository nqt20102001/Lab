using Lab.Application.DTOs;
using Lab.Application.Interfaces;
using Lab.Domain.Entities;
using Lab.Domain.Interfaces;

namespace Lab.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        // GET ALL CATALOGS
        public async Task<IEnumerable<CatalogDTO>> GetAllCatalogsAsync()
        {
            var catalogs = await _catalogRepository.GetAllAsync();

            var catalogDtos = catalogs.Select(c => new CatalogDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                IsActive = c.IsActive,
                Books = c.BookCatalogs?.Select(bc => new BookDTO
                {
                    Id = bc.Book.Id,
                    Title = bc.Book.Title,
                    Price = bc.Book.Price,
                    Author = bc.Book.Author,
                    Publisher = bc.Book.Publisher,
                    CreatedDate = bc.Book.CreatedDate,
                    Available = bc.Book.Available,
                    GenreName = bc.Book.Genre?.Name,
                    CatalogTitles = bc.Book.BookCatalogs?.Select(bc2 => bc2.Catalog.Title).ToList()
                }).ToList()
            });

            return catalogDtos;
        }

        // GET CATALOG BY ID
        public async Task<CatalogDTO> GetCatalogByIdAsync(int id)
        {
            var catalog = await _catalogRepository.GetByIdAsync(id);

            if (catalog == null)
                return null;

            var catalogDto = new CatalogDTO
            {
                Id = catalog.Id,
                Title = catalog.Title,
                Description = catalog.Description,
                IsActive = catalog.IsActive,
                Books = catalog.BookCatalogs?.Select(bc => new BookDTO
                {
                    Id = bc.Book.Id,
                    Title = bc.Book.Title,
                    Price = bc.Book.Price,
                    Author = bc.Book.Author,
                    Publisher = bc.Book.Publisher,
                    CreatedDate = bc.Book.CreatedDate,
                    Available = bc.Book.Available,
                    GenreName = bc.Book.Genre?.Name,
                    CatalogTitles = bc.Book.BookCatalogs?.Select(bc2 => bc2.Catalog.Title).ToList()
                }).ToList()
            };

            return catalogDto;
        }

        // ADD NEW CATALOG
        public async Task AddCatalogAsync(CatalogDTO catalogDto)
        {
            var catalog = new Catalog
            {
                Title = catalogDto.Title,
                Description = catalogDto.Description,
                IsActive = catalogDto.IsActive
                // Nếu cần thêm BookCatalogs, bạn có thể xử lý ở đây
            };

            await _catalogRepository.AddAsync(catalog);
        }

        // UPDATE EXISTING CATALOG
        public async Task UpdateCatalogAsync(CatalogDTO catalogDto)
        {
            var catalog = await _catalogRepository.GetByIdAsync(catalogDto.Id);

            if (catalog != null)
            {
                catalog.Title = catalogDto.Title;
                catalog.Description = catalogDto.Description;
                catalog.IsActive = catalogDto.IsActive;
                // Cập nhật BookCatalogs nếu cần

                await _catalogRepository.UpdateAsync(catalog);
            }
        }

        // DELETE CATALOG
        public async Task DeleteCatalogAsync(int id)
        {
            await _catalogRepository.DeleteAsync(id);
        }
    }
}
