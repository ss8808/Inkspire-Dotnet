using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task AddToCartAsync(int userId, AddToCartDto dto);
        Task<bool> UpdateCartItemAsync(int userId, UpdateCartItemDto dto);

        Task<bool> RemoveFromCartAsync(int userId, int bookId);
    }
}
