using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<AnnouncementDto>> GetAllAsync();
        Task<AnnouncementDto?> GetByIdAsync(int id);
        Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto, int createdBy);
        Task<bool> UpdateAsync(int id, CreateAnnouncementDto dto, int createdBy);

        Task<bool> DeleteAsync(int id);
    }
}
