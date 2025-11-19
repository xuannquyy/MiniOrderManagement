// OrderCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace MiniOrderManagement.DTOs
{
    public class OrderItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

    public class OrderCreateDto
    {
        // For simplicity: we take CustomerId as the Identity user id (string)
        public string CustomerId { get; set; } 

        [Required, MinLength(1)]
        public List<OrderItemCreateDto> Items { get; set; }
    }
}
