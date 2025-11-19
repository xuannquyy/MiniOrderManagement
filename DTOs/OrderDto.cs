// OrderDto.cs
namespace MiniOrderManagement.DTOs
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new();
    }
}
