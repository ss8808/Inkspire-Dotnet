using Inksprie_Backend.Data;
using Inksprie_Backend.Dtos;
using Inksprie_Backend.Entities;
using Inksprie_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inksprie_Backend.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsPaymentDone);

            if (cart == null)
                throw new KeyNotFoundException("Cart not found for user.");

            return new CartDto
            {
                CartId = cart.Id,
                IsPaymentDone = cart.IsPaymentDone,
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt,
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    BookId = ci.BookId,
                    Quantity = ci.Quantity,
                    AddedAt = ci.AddedAt
                }).ToList()
            };
        }

        public async Task AddToCartAsync(int userId, AddToCartDto dto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsPaymentDone);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsPaymentDone = false
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(); // get Cart ID
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == dto.BookId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                existingItem.AddedAt = DateTime.UtcNow;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    BookId = dto.BookId,
                    Quantity = dto.Quantity,
                    AddedAt = DateTime.UtcNow
                });
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int bookId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsPaymentDone);

            if (cart == null)
                return false;

            var item = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (item == null)
                return false;

            cart.CartItems.Remove(item);
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCartItemAsync(int userId, UpdateCartItemDto dto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsPaymentDone);

            if (cart == null)
                return false;

            var item = cart.CartItems.FirstOrDefault(ci => ci.BookId == dto.BookId);

            if (item != null)
            {
                if (dto.Quantity <= 0)
                {
                    cart.CartItems.Remove(item); // Optionally remove
                }
                else
                {
                    item.Quantity = dto.Quantity;
                    item.AddedAt = DateTime.UtcNow;
                }
            }
            else
            {
                if (dto.Quantity > 0)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        BookId = dto.BookId,
                        Quantity = dto.Quantity,
                        AddedAt = DateTime.UtcNow
                    });
                }
                // If quantity is 0 or less and item doesn't exist, we do nothing
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
