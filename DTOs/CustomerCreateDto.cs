// CustomerCreateDto.cs
using System.ComponentModel.DataAnnotations;
namespace MiniOrderManagement.DTOs
{
    public class CustomerCreateDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        [Phone, MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }
    }
}
