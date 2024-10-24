
namespace Lab.Application.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CartDetailDTO> CartDetails { get; set; }
        public UserDTO User { get; set; }
    }
}
