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

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpGet("all-history")]
        public async Task<IActionResult> GetAllOrderHistory()
        {
            var result = await _orderService.GetAllOrderHistoryAsync();
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpGet("user-history")]
        public async Task<IActionResult> GetUserOrderHistory()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var result = await _orderService.GetUserOrderHistoryAsync(userId);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Identity.Bearer")]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] string claimCode)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var result = await _orderService.CancelOrderAsync(claimCode, userId);
            return result ? Ok(new { message = "Order cancelled." }) : BadRequest("Order cannot be cancelled.");
        }







        [HttpGet("test")]
        public IActionResult Test() => Ok("OrderController is working!");

    }
}
