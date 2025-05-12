using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookDetailsDto>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Inventory)
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Description = b.Description,
                    AuthorName = b.AuthorName,
                    PublisherName = b.PublisherName,
                    Price = b.Price,
                    Format = b.Format,
                    Language = b.Language,
                    PublicationDate = b.PublicationDate,
                    PageCount = b.PageCount,
                    IsBestseller = b.IsBestseller,
                    IsAwardWinner = b.IsAwardWinner,
                    IsNewRelease = b.IsNewRelease,
                    NewArrival = b.NewArrival,
                    CommingSoon = b.CommingSoon,
                    CoverImageUrl = b.CoverImageUrl,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    IsActive = b.IsActive,

                    QuantityInStock = b.Inventory != null ? b.Inventory.QuantityInStock : null,
                    LastStockedDate = b.Inventory != null ? b.Inventory.LastStockedDate : null,
                    ReorderThreshold = b.Inventory != null ? b.Inventory.ReorderThreshold : null,
                    IsAvailable = b.Inventory != null ? b.Inventory.IsAvailable : null
                })
                .ToListAsync();
        }


        public async Task<BookDetailsDto?> GetByIdAsync(int id)
        {
            var b = await _context.Books
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (b == null) return null;

            return new BookDetailsDto
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                Description = b.Description,
                AuthorName = b.AuthorName,
                PublisherName = b.PublisherName,
                Price = b.Price,
                Format = b.Format,
                Language = b.Language,
                PublicationDate = b.PublicationDate,
                PageCount = b.PageCount,
                IsBestseller = b.IsBestseller,
                IsAwardWinner = b.IsAwardWinner,
                IsNewRelease = b.IsNewRelease,
                NewArrival = b.NewArrival,
                CommingSoon = b.CommingSoon,
                CoverImageUrl = b.CoverImageUrl,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt,
                IsActive = b.IsActive,

                QuantityInStock = b.Inventory?.QuantityInStock,
                LastStockedDate = b.Inventory?.LastStockedDate,
                ReorderThreshold = b.Inventory?.ReorderThreshold,
                IsAvailable = b.Inventory?.IsAvailable
            };
        }



        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Description = dto.Description,
                AuthorName = dto.AuthorName,
                PublisherName = dto.PublisherName,
                Price = dto.Price,
                Format = dto.Format,
                Language = dto.Language,
                PublicationDate = dto.PublicationDate,
                PageCount = dto.PageCount,
                IsBestseller = dto.IsBestseller,
                IsAwardWinner = dto.IsAwardWinner,
                IsNewRelease = dto.IsNewRelease,
                NewArrival = dto.NewArrival,
                CommingSoon = dto.CommingSoon,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = dto.IsActive,
            };

            if (dto.CoverImageUrl != null)
            {
                book.CoverImageUrl = await SaveImageAsync(dto.CoverImageUrl);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var inventory = new Inventory
            {
                BookId = book.Id,
                QuantityInStock = dto.QuantityInStock,
                ReorderThreshold = dto.ReorderThreshold,
                LastStockedDate = DateTime.UtcNow,
                IsAvailable = dto.QuantityInStock > 0
            };
            _context.Inventories.Add(inventory);

            await _context.SaveChangesAsync();

            return new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                AuthorName = book.AuthorName,
                PublisherName = book.PublisherName,
                Price = book.Price,
                CoverImageUrl = book.CoverImageUrl ?? "",
                PublicationDate = book.PublicationDate
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await _context.Books
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return false;

            // 🟡 Update Book properties
            book.ISBN = dto.ISBN ?? book.ISBN;
            book.Title = dto.Title ?? book.Title;
            book.Description = dto.Description ?? book.Description;
            book.AuthorName = dto.AuthorName ?? book.AuthorName;
            book.PublisherName = dto.PublisherName ?? book.PublisherName;
            book.Price = dto.Price ?? book.Price;
            book.Format = dto.Format ?? book.Format;
            book.Language = dto.Language ?? book.Language;
            book.PublicationDate = dto.PublicationDate ?? book.PublicationDate;
            book.PageCount = dto.PageCount ?? book.PageCount;
            book.IsBestseller = dto.IsBestseller ?? book.IsBestseller;
            book.IsAwardWinner = dto.IsAwardWinner ?? book.IsAwardWinner;
            book.IsNewRelease = dto.IsNewRelease ?? book.IsNewRelease;
            book.NewArrival = dto.NewArrival ?? book.NewArrival;
            book.CommingSoon = dto.CommingSoon ?? book.CommingSoon;
            book.IsActive = dto.IsActive ?? book.IsActive;
            book.UpdatedAt = DateTime.UtcNow;

            if (dto.CoverImage != null)
                book.CoverImageUrl = await SaveImageAsync(dto.CoverImage);

            // ✅ Update inventory if applicable
            if (book.Inventory != null)
            {
                if (dto.QuantityInStock.HasValue)
                    book.Inventory.QuantityInStock = dto.QuantityInStock.Value;

                if (dto.ReorderThreshold.HasValue)
                    book.Inventory.ReorderThreshold = dto.ReorderThreshold.Value;

                if (dto.QuantityInStock.HasValue)
                {
                    book.Inventory.LastStockedDate = DateTime.UtcNow;
                    book.Inventory.IsAvailable = dto.QuantityInStock.Value > 0;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/images/{uniqueName}";
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
