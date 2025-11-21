using MiniOrderManagement.DTOs;
using MiniOrderManagement.Models;

namespace MiniOrderManagement.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId, CreateOrderDto dto);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(); // Cho Admin
    }
}