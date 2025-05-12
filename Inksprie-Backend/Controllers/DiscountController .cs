using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAllAsync();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost("{adminId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int adminId, CreateDiscountDto dto)
        {
            var created = await _discountService.CreateAsync(dto, adminId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}/by/{adminId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, int adminId, CreateDiscountDto dto)
        {
            var updated = await _discountService.UpdateAsync(id, dto, adminId);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _discountService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
