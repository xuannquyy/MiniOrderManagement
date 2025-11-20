using System.ComponentModel.DataAnnotations;

namespace MiniOrderManagement.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
