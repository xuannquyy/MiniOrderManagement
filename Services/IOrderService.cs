using MiniOrderManagement.DTOs;

namespace MiniOrderManagement.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderCreateDto dto);
        Task<OrderDto> GetOrderAsync(int id);
        Task<List<OrderDto>> GetOrdersByCustomerAsync(string customerId);
    }
}
