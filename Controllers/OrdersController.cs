using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.DTOs;
using MiniOrderManagement.Services;
using System.Security.Claims;

namespace MiniOrderManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Mặc định phải login mới vào được
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            try
            {
                // Lấy ID user từ Token (Claim)
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                var order = await _orderService.CreateOrderAsync(userId, dto);
                return Ok(new { Message = "Order created successfully", OrderId = order.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("all-orders")]
        [Authorize(Roles = "Admin")] // Chỉ admin mới xem hết
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}