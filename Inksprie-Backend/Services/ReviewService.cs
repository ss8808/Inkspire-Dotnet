using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Enum;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddReviewAsync(int userId, ReviewDto reviewDto)
        {
            var hasPurchased = await _context.Orders
                .Include(o => o.OrderItems)
                .AnyAsync(o => o.UserId == userId && o.OrderItems.Any(i => i.BookId == reviewDto.BookId) && o.Status == OrderStatus.Completed);

            if (!hasPurchased)
                return false;

            var review = new Review
            {
                UserId = userId,
                BookId = reviewDto.BookId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ReviewDate = DateTime.UtcNow,
                IsVerifiedPurchase = true
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
