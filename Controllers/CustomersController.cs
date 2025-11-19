using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Data;
using MiniOrderManagement.DTOs;
using MiniOrderManagement.Models;

namespace MiniOrderManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public CustomersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Customers.ToListAsync();
            return Ok(_mapper.Map<List<CustomerDto>>(list));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var c = await _db.Customers.FindAsync(id);
            if (c == null) return NotFound();
            return Ok(_mapper.Map<CustomerDto>(c));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<Customer>(dto);
            _db.Customers.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<CustomerDto>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var c = await _db.Customers.FindAsync(id);
            if (c == null) return NotFound();
            _mapper.Map(dto, c);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<CustomerDto>(c));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Customers.FindAsync(id);
            if (c == null) return NotFound();
            _db.Customers.Remove(c);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
