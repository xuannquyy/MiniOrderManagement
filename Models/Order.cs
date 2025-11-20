using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniOrderManagement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; } // FK
        public Customer Customer { get; set; } // Navigation property

        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
