using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create([FromForm] CreateBookDto dto)
        {
            var created = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateBookDto dto)
        {
            var updated = await _bookService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok("Deleted Successfully.");
        }
    }
}
