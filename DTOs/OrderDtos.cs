using System.ComponentModel.DataAnnotations;

namespace MiniOrderManagement.DTOs
{
    // Dùng để trả về dữ liệu (GET)
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDto> Details { get; set; }
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    // Dùng để nhận dữ liệu (POST)
    public class CreateOrderDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<CartItemDto> Items { get; set; }
    }

    public class CartItemDto
    {
        [Required]
        public int ProductId { get; set; }
        
        [Range(1, 100, ErrorMessage = "Quantity must be > 0")]
        public int Quantity { get; set; }
    }
}