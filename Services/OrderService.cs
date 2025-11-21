using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Data;
using MiniOrderManagement.DTOs;
using MiniOrderManagement.Models;

namespace MiniOrderManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(string userId, CreateOrderDto dto)
        {
            // 1. Start Transaction (đảm bảo toàn vẹn dữ liệu)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    OrderDetails = new List<OrderDetail>()
                };

                decimal totalAmount = 0;

                foreach (var item in dto.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    
                    // Validate logic backend
                    if (product == null) throw new Exception($"Product ID {item.ProductId} not found");
                    if (product.Stock < item.Quantity) throw new Exception($"Product {product.Name} not enough stock");

                    // Trừ tồn kho
                    product.Stock -= item.Quantity;

                    var detail = new OrderDetail
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = item.Quantity,
                        PriceAtOrder = product.Price // Lưu giá tại thời điểm mua
                    };
                    
                    totalAmount += (detail.PriceAtOrder * detail.Quantity);
                    order.OrderDetails.Add(detail);
                }

                order.TotalAmount = totalAmount;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return MapToDto(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return MapToDto(orders);
        }

        // Helper function map tay (hoặc dùng AutoMapper nếu em thạo)
        private List<OrderDto> MapToDto(List<Order> orders)
        {
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                Details = o.OrderDetails.Select(d => new OrderDetailDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.Product.Name,
                    Quantity = d.Quantity,
                    UnitPrice = d.PriceAtOrder
                }).ToList()
            }).ToList();
        }
    }
}