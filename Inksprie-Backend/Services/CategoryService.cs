using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.Name = dto.Name;
            category.Description = dto.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignCategoryToBookAsync(int bookId, int categoryId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var category = await _context.Categories.FindAsync(categoryId);

            if (book == null || category == null)
                return false;

            var exists = await _context.BookCategories
                .AnyAsync(x => x.BookId == bookId && x.CategoryId == categoryId);

            if (exists)
                return false;

            _context.BookCategories.Add(new BookCategory
            {
                BookId = bookId,
                CategoryId = categoryId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDto>?> GetBooksByCategoryAsync(int categoryId)
        {
            var exists = await _context.Categories.AnyAsync(x => x.Id == categoryId);
            if (!exists) return null;

            var books = await _context.BookCategories
                .Where(bc => bc.CategoryId == categoryId)
                .Include(bc => bc.Book)
                .Select(bc => new BookDto
                {
                    Id = bc.Book.Id,
                    ISBN = bc.Book.ISBN,
                    Title = bc.Book.Title,
                    AuthorName = bc.Book.AuthorName,
                    PublisherName = bc.Book.PublisherName,
                    Price = bc.Book.Price,
                    CoverImageUrl = bc.Book.CoverImageUrl,
                    PublicationDate = bc.Book.PublicationDate
                }).ToListAsync();

            return books;
        }



    }
}
