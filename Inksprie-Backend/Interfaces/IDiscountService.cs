using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IDiscountService
    {
        Task<List<DiscountDto>> GetAllAsync();
        Task<DiscountDto?> GetByIdAsync(int id);
        Task<DiscountDto> CreateAsync(CreateDiscountDto dto, int adminId);
        Task<bool> UpdateAsync(int id, CreateDiscountDto dto, int adminId);
        Task<bool> DeleteAsync(int id);
        Task<List<DiscountedBookDto>> GetDiscountedBooksAsync();
        Task<DiscountedBookDto?> GetActiveDiscountByBookIdAsync(int bookId);


    }
}
