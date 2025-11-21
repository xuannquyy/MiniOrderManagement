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
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Products.ToListAsync();
            return Ok(_mapper.Map<List<ProductDto>>(list));
        }

        // GET: api/products/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(p));
        }

        // POST: api/products
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = _mapper.Map<Product>(dto);
            _db.Products.Add(prod);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<ProductDto>(prod);
            return CreatedAtAction(nameof(Get), new { id = prod.Id }, result);
        }

        // PUT: api/products/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = await _db.Products.FindAsync(id);
            if (prod == null) return NotFound();
            _mapper.Map(dto, prod);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<ProductDto>(prod));
        }

        // DELETE: api/products/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _db.Products.FindAsync(id);
            if (prod == null) return NotFound();
            _db.Products.Remove(prod);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}