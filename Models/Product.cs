using System.ComponentModel.DataAnnotations;

namespace MiniOrderManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string? Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}