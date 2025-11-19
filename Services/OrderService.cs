using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Data;
using MiniOrderManagement.DTOs;
using MiniOrderManagement.Models;

namespace MiniOrderManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new ArgumentException("Order must contain at least one item.");

            var products = await _db.Products
                .Where(p => dto.Items.Select(i => i.ProductId).Contains(p.Id))
                .ToListAsync();

            // Validate all items exist and stock
            foreach (var item in dto.Items)
            {
                var prod = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (prod == null) throw new KeyNotFoundException($"Product {item.ProductId} not found.");
                if (item.Quantity <= 0) throw new ArgumentException("Quantity must be > 0.");
                if (prod.Stock < item.Quantity) throw new InvalidOperationException($"Not enough stock for product {prod.Id}.");
            }

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            decimal total = 0m;
            foreach (var item in dto.Items)
            {
                var prod = products.First(p => p.Id == item.ProductId);
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = prod.Id,
                    Quantity = item.Quantity,
                    PriceAtOrder = prod.Price
                });
                prod.Stock -= item.Quantity; // decrease stock
                total += prod.Price * item.Quantity;
            }
            order.Total = total;

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // Load navigation for mapping
            await _db.Entry(order).Collection(o => o.OrderDetails).LoadAsync();
            foreach (var od in order.OrderDetails)
                await _db.Entry(od).Reference(o => o.Product).LoadAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var order = await _db.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return null;
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetOrdersByCustomerAsync(string customerId)
        {
            var orders = await _db.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
