using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Enum;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Inksprie_Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public OrderService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(int userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(i => i.Book)
                                           .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty.");

            var totalBooks = cart.CartItems.Sum(ci => ci.Quantity);
            var totalAmount = cart.CartItems.Sum(ci => ci.Book.Price * ci.Quantity);

            // Discount logic
            var pastOrders = await _context.Orders.Where(o => o.UserId == userId && o.Status == OrderStatus.Completed).CountAsync();
            decimal discount = 0;
            if (totalBooks >= 5) discount += 0.05m;
            if (pastOrders >= 10) discount += 0.10m;

            var discountAmount = totalAmount * discount;
            var finalAmount = totalAmount - discountAmount;
            var claimCode = GenerateClaimCode();

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                DiscountApplied = discount > 0,
                DiscountAmount = discountAmount,
                FinalAmount = finalAmount,
                Status = OrderStatus.ReadyForPickup,
                ClaimCode = claimCode,
                UpdatedAt = DateTime.UtcNow,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    BookId = ci.BookId,
                    Quantity = ci.Quantity,
                    PriceAtOrder = ci.Book.Price,
                    Subtotal = ci.Book.Price * ci.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            await _emailService.SendClaimCodeEmailAsync(userId, claimCode, finalAmount);

            return new OrderResponseDto { OrderId = order.Id, FinalAmount = finalAmount, ClaimCode = claimCode };
        }

        private string GenerateClaimCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[6];
            rng.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").Substring(0, 10);
        }

        public async Task<bool> CompleteOrderAsync(string claimCode, int adminUserId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.ClaimCode == claimCode && o.Status == OrderStatus.ReadyForPickup);

            if (order == null) return false;

            order.Status = OrderStatus.Completed;
            order.PickupDate = DateTime.UtcNow;
            order.ProcessedBy = adminUserId;

            // Save to Purchase table
            foreach (var item in order.OrderItems)
            {
                _context.Purchases.Add(new Purchase
                {
                    UserId = order.UserId,
                    BookId = item.BookId,
                    PurchaseDate = DateTime.UtcNow,
                    OrderId = order.Id
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<OrderHistoryDto>> GetAllOrderHistoryAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(i => i.Book)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderHistoryDto
                {
                    OrderId = o.Id,
                    ClaimCode = o.ClaimCode,
                    FinalAmount = o.FinalAmount,
                    Status = o.Status.ToString(),
                    OrderDate = o.OrderDate,
                    MemberName = o.User.UserName,
                    MemberEmail = o.User.Email,
                    Books = o.OrderItems.Select(i => new OrderedBookDto
                    {
                        Title = i.Book.Title,
                        Price = i.PriceAtOrder,
                        Quantity = i.Quantity,
                        CoverImageUrl = i.Book.CoverImageUrl
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderHistoryDto>> GetUserOrderHistoryAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(i => i.Book)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderHistoryDto
                {
                    OrderId = o.Id,
                    ClaimCode = o.ClaimCode,
                    FinalAmount = o.FinalAmount,
                    Status = o.Status.ToString(),
                    OrderDate = o.OrderDate,
                    MemberName = o.User.UserName,
                    MemberEmail = o.User.Email,
                    Books = o.OrderItems.Select(i => new OrderedBookDto
                    {
                        Title = i.Book.Title,
                        Price = i.PriceAtOrder,
                        Quantity = i.Quantity,
                        CoverImageUrl = i.Book.CoverImageUrl
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> CancelOrderAsync(string claimCode, int userId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.ClaimCode == claimCode && o.UserId == userId && o.Status != OrderStatus.Completed);

            if (order == null || order.Status == OrderStatus.Completed)
                return false;

            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
