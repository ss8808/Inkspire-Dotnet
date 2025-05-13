using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DiscountDto>> GetAllAsync()
        {
            return await _context.Discounts
                .Select(d => new DiscountDto
                {
                    Id = d.Id,
                    BookId = d.BookId,
                    UserId = d.UserId,
                    DiscountPercentage = d.DiscountPercentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    OnSale = d.OnSale,
                    IsActive = d.IsActive
                }).ToListAsync();
        }

        public async Task<DiscountDto?> GetByIdAsync(int id)
        {
            var d = await _context.Discounts.FindAsync(id);
            if (d == null) return null;

            return new DiscountDto
            {
                Id = d.Id,
                BookId = d.BookId,
                UserId = d.UserId,
                DiscountPercentage = d.DiscountPercentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                OnSale = d.OnSale,
                IsActive = d.IsActive
            };
        }

        public async Task<DiscountDto> CreateAsync(CreateDiscountDto dto, int adminId)
        {
            var discount = new Discount
            {
                BookId = dto.BookId,
                UserId = adminId,
                DiscountPercentage = dto.DiscountPercentage,
                //StartDate = dto.StartDate,
                //EndDate = dto.EndDate,
                StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc),
                OnSale = dto.OnSale,
                IsActive = dto.IsActive
            };

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return new DiscountDto
            {
                Id = discount.Id,
                BookId = discount.BookId,
                UserId = discount.UserId,
                DiscountPercentage = discount.DiscountPercentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                OnSale = discount.OnSale,
                IsActive = discount.IsActive
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateDiscountDto dto, int adminId)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return false;

            discount.BookId = dto.BookId;
            discount.DiscountPercentage = dto.DiscountPercentage;
            discount.StartDate = dto.StartDate;
            //discount.EndDate = dto.EndDate;
            //discount.OnSale = dto.OnSale;
            discount.StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc);
            discount.EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc);

            discount.IsActive = dto.IsActive;
            discount.UserId = adminId;

            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return false;

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<DiscountedBookDto>> GetDiscountedBooksAsync()
        {
            var now = DateTime.UtcNow;

            return await _context.Discounts
                .Where(d => d.IsActive && d.OnSale && d.EndDate >= now)
                .Include(d => d.Book)
                    .ThenInclude(b => b.Inventory)
                .Select(d => new DiscountedBookDto
                {
                    BookId = d.BookId,
                    Title = d.Book.Title,
                    AuthorName = d.Book.AuthorName,
                    OriginalPrice = d.Book.Price,
                    DiscountedPrice = d.Book.Price - (d.Book.Price * d.DiscountPercentage / 100),
                    CoverImageUrl = d.Book.CoverImageUrl,

                    DiscountPercentage = d.DiscountPercentage,
                    DiscountStart = d.StartDate,
                    DiscountEnd = d.EndDate,
                    OnSale = d.OnSale,
                    IsActive = d.IsActive,

                    QuantityInStock = d.Book.Inventory != null ? d.Book.Inventory.QuantityInStock : 0
                })
                .ToListAsync();
        }

        public async Task<DiscountedBookDto?> GetActiveDiscountByBookIdAsync(int bookId)
        {
            var discount = await _context.Discounts
                .Include(d => d.Book)
                    .ThenInclude(b => b.Inventory)
                .Where(d => d.BookId == bookId && d.IsActive && d.OnSale)
                .OrderByDescending(d => d.DiscountPercentage)
                .FirstOrDefaultAsync();

            if (discount == null) return null;

            return new DiscountedBookDto
            {
                BookId = discount.BookId,
                Title = discount.Book.Title,
                AuthorName = discount.Book.AuthorName,
                OriginalPrice = discount.Book.Price,
                DiscountedPrice = discount.Book.Price - (discount.Book.Price * discount.DiscountPercentage / 100),
                CoverImageUrl = discount.Book.CoverImageUrl,

                DiscountPercentage = discount.DiscountPercentage,
                DiscountStart = discount.StartDate,
                DiscountEnd = discount.EndDate,
                OnSale = discount.OnSale,
                IsActive = discount.IsActive,

                QuantityInStock = discount.Book.Inventory?.QuantityInStock ?? 0
            };
        }





    }
}
