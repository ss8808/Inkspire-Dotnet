using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            try
            {
                var cart = await _cartService.GetCartAsync(userId);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] AddToCartDto dto)
        {
            await _cartService.AddToCartAsync(userId, dto);
            return Ok("Item added to cart.");
        }

        [HttpDelete("{userId}/item/{bookId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int bookId)
        {
            var result = await _cartService.RemoveFromCartAsync(userId, bookId);
            return result ? NoContent() : NotFound("Item not found in cart.");
        }

        [HttpPut("{userId}/item")]
        public async Task<IActionResult> UpdateCartItem(int userId, [FromBody] UpdateCartItemDto dto)
        {
            var result = await _cartService.UpdateCartItemAsync(userId, dto);
            return result ? Ok("Cart item updated or added.") : NotFound("Cart does not exist.");
        }



    }
}
