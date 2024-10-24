
namespace Lab.Domain.Entities
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<BookCatalog> BookCatalogs { get; set; }
    }
}
