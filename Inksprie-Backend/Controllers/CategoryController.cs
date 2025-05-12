using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Inksprie_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var created = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Update(int id, CreateCategoryDto dto)
        {
            var updated = await _categoryService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }


        [HttpPost("{bookId}/assign")]
        //[Authorize]
        public async Task<IActionResult> AssignCategoryToBook(int bookId, [FromBody] int categoryId)
        {
            var success = await _categoryService.AssignCategoryToBookAsync(bookId, categoryId);
            if (!success) return BadRequest("Book or Category not found, or already assigned.");
            return Ok("Category assigned to book.");
        }

        [HttpGet("{categoryId}/books")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _categoryService.GetBooksByCategoryAsync(categoryId);
            if (books == null || !books.Any()) return NotFound("No books found for this category.");
            return Ok(books);
        }



    }
}
