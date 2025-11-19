using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Services;
using MiniOrderManagement.DTOs;
using System.ComponentModel.DataAnnotations;


namespace MiniOrderManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Admin can get any order; User only their own.
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null) return NotFound();

            // If user is not Admin, check ownership
            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (order.CustomerId != userId) return Forbid();
            }
            return Ok(order);
        }

        // User creates order (or Admin can create)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // If user is not Admin, force CustomerId from token
            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                dto.CustomerId = userId;
            }

            try
            {
                var created = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        // Admin list all orders
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            // for brevity, get all order ids and map via service (or implement method)
            return Ok("Implement if needed");
        }

        // Additional endpoints (update status) - Admin only
        [HttpPut("{id:int}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest req)
        {
            // minimal impl: load order, change status
            // omitted for brevity — implement similarly
            return NoContent();
        }

        public class UpdateStatusRequest
        {
            [Required]
            public string Status { get; set; }
        }
    }
}
