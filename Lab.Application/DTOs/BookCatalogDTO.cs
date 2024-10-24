namespace Lab.Application.DTOs
{
    public class BookCatalogDTO
    {
        public int BookId { get; set; }
        public int CatalogId { get; set; }
        public BookDTO Book { get; set; }
        public CatalogDTO Catalog { get; set; }
    }
}
