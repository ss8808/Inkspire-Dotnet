using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDetailsDto>> GetAllAsync();
        Task<BookDetailsDto?> GetByIdAsync(int id);

        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task<bool> DeleteAsync(int id);
        Task<string> SaveImageAsync(IFormFile imageFile);
        Task<bool> UpdateAsync(int id, UpdateBookDto dto);
    }
}
