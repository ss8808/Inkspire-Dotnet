using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Create(int userId, CreateBookmarkDto dto)
        {
            try
            {
                var result = await _bookmarkService.CreateAsync(userId, dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            try
            {
                var bookmarks = await _bookmarkService.GetAllByUserIdAsync(userId);
                return Ok(bookmarks);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // User does not exist
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // No bookmarks for user
            }
        }


        [HttpDelete("{bookmarkId}/user/{userId}")]
        public async Task<IActionResult> Delete(int bookmarkId, int userId)
        {
            try
            {
                var success = await _bookmarkService.DeleteAsync(bookmarkId, userId);
                return Ok("Bookmark Deleted Successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
