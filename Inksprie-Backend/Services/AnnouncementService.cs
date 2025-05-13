using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnnouncementDto>> GetAllAsync()
        {
            return await _context.Announcements
                .Select(a => new AnnouncementDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    CreatedBy = a.CreatedBy,
                    IsActive = a.IsActive
                }).ToListAsync();
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            var a = await _context.Announcements.FindAsync(id);
            if (a == null) return null;

            return new AnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                CreatedBy = a.CreatedBy,
                IsActive = a.IsActive
            };
        }

        public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto dto, int createdBy)
        {
            var announcement = new Announcement
            {
                Title = dto.Title,
                Content = dto.Content,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedBy = createdBy,
                IsActive = dto.IsActive
            };

            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            return new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Content = announcement.Content,
                StartDate = DateTime.SpecifyKind(announcement.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(announcement.EndDate, DateTimeKind.Utc),
                CreatedBy = announcement.CreatedBy,
                IsActive = announcement.IsActive
            };

        }

        public async Task<bool> UpdateAsync(int id, CreateAnnouncementDto dto, int _)
        {
            const int defaultAdminId = 1;

            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return false;

            announcement.Title = dto.Title;
            announcement.Content = dto.Content;
            announcement.StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc);
            announcement.EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc);
            announcement.CreatedBy = defaultAdminId;
            announcement.IsActive = dto.IsActive;

            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return false;

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
