
namespace Lab.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Publisher { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<BookCatalog> BookCatalogs { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
