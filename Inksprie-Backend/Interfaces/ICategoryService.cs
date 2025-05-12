using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(int id, CreateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AssignCategoryToBookAsync(int bookId, int categoryId);
        Task<List<BookDto>?> GetBooksByCategoryAsync(int categoryId);

    }
}
