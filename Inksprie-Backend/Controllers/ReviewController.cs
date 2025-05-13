using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(new { message = "User ID not found or invalid." });

            var success = await _reviewService.AddReviewAsync(userId, dto);
            if (!success)
                return StatusCode(403, new { message = "You can only review books you've purchased." });

            return Ok(new { message = "Review submitted successfully." });
        }

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpGet("has-reviewed/{bookId}")]
        public async Task<IActionResult> HasUserReviewed(int bookId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var hasReviewed = await _reviewService.HasUserReviewedAsync(userId, bookId);
            return Ok(new { hasReviewed });
        }



    }
}
