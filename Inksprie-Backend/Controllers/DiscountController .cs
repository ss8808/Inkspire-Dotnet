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

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDiscountDto dto)
        {
            const int defaultAdminId = 1;

            try
            {
                var created = await _discountService.CreateAsync(dto, defaultAdminId);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR CREATING DISCOUNT: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("🔍 INNER EXCEPTION: " + ex.InnerException.Message);
                }

                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error from the middleware.",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDiscountDto dto)
        {
            const int defaultAdminId = 1;
            try
            {
                var updated = await _discountService.UpdateAsync(id, dto, defaultAdminId);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR UPDATING DISCOUNT: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("🔍 INNER EXCEPTION: " + ex.InnerException.Message);
                }

                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error from the middleware.",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _discountService.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR DELETING DISCOUNT: " + ex.Message);
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error while deleting.",
                    Detailed = ex.InnerException?.Message ?? ex.Message
                });
            }


        }

        [HttpGet("discounted-books")]
        public async Task<IActionResult> GetDiscountedBooks()
        {
            var discountedBooks = await _discountService.GetDiscountedBooksAsync();
            return Ok(discountedBooks);
        }
    }
}
