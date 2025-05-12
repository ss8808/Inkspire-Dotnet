using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ApplicationDbContext _context;

        public BookmarkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookmarkDto> CreateAsync(int userId, CreateBookmarkDto dto)
        {
            // ✅ Check if user exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new ArgumentException("User does not exist.");

            // ✅ Check if book exists
            var bookExists = await _context.Books.AnyAsync(b => b.Id == dto.BookId);
            if (!bookExists)
                throw new ArgumentException("Book does not exist.");

            // ✅ Prevent duplicate bookmarks
            var alreadyBookmarked = await _context.Bookmarks
                .AnyAsync(b => b.UserId == userId && b.BookId == dto.BookId);
            if (alreadyBookmarked)
                throw new InvalidOperationException("Book already bookmarked by this user.");

            // ✅ Create bookmark
            var bookmark = new Bookmark
            {
                UserId = userId,
                BookId = dto.BookId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();

            return new BookmarkDto
            {
                Id = bookmark.Id,
                UserId = bookmark.UserId,
                BookId = bookmark.BookId,
                CreatedAt = bookmark.CreatedAt
            };
        }

        public async Task<List<BookmarkDto>> GetAllByUserIdAsync(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new ArgumentException("User does not exist.");

            var bookmarks = await _context.Bookmarks
                .Where(b => b.UserId == userId)
                .Select(b => new BookmarkDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    BookId = b.BookId,
                    CreatedAt = b.CreatedAt
                }).ToListAsync();

            if (!bookmarks.Any())
                throw new KeyNotFoundException("No bookmarks found for this user.");

            return bookmarks;
        }


        public async Task<bool> DeleteAsync(int bookmarkId, int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new ArgumentException("User does not exist.");

            var bookmark = await _context.Bookmarks
                .FirstOrDefaultAsync(b => b.Id == bookmarkId && b.UserId == userId);

            if (bookmark == null)
                throw new KeyNotFoundException("Bookmark not found for this user.");

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
