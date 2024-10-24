namespace Lab.Application.DTOs
{
    public class CartDetailDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public BookDTO Book { get; set; }
        public CartDTO Cart { get; set; }
    }
}
