
namespace Lab.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
