using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IBookmarkService
    {
        Task<List<BookmarkDto>> GetAllByUserIdAsync(int userId);
        Task<BookmarkDto> CreateAsync(int userId, CreateBookmarkDto dto);
        Task<bool> DeleteAsync(int bookmarkId, int userId);
    }
}
