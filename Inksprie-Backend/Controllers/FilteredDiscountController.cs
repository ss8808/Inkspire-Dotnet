using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilteredDiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public FilteredDiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("active-discounts")]
        public async Task<IActionResult> GetDiscountedBooks()
        {
            var result = await _discountService.GetDiscountedBooksAsync();
            return Ok(result);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetActiveDiscountByBookId(int bookId)
        {
            var result = await _discountService.GetActiveDiscountByBookIdAsync(bookId);
            if (result == null) return NotFound("No active discount for this book.");
            return Ok(result);
        }

    }
}
