// OrderDto.cs
namespace MiniOrderManagement.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public List<OrderDetailCreateDto> Items { get; set; } = new();
    }

    public class OrderDetailCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

