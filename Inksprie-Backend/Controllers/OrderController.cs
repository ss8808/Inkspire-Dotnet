using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inksprie_Backend.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(new { message = "User ID not found or invalid." });

            var result = await _orderService.PlaceOrderAsync(userId);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPost("complete")]
        public async Task<IActionResult> CompleteOrder([FromBody] string claimCode)
        {
            var adminIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(adminIdStr, out int adminId))
                return Unauthorized();

            var success = await _orderService.CompleteOrderAsync(claimCode, adminId);
            if (!success)
                return NotFound("Order not found or already completed.");

            return Ok(new { message = "Order marked as completed." });
        }






        [HttpGet("test")]
        public IActionResult Test() => Ok("OrderController is working!");

    }
}
