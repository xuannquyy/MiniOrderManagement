using System.ComponentModel.DataAnnotations;

namespace MiniOrderManagement.DTOs
{
    // Dùng để trả về dữ liệu (GET)
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    // Dùng để tạo mới hoặc cập nhật (POST/PUT)
    public class CustomerCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Phone, MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }
    }
}