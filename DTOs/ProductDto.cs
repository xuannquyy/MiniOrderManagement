using System.ComponentModel.DataAnnotations;

namespace MiniOrderManagement.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}