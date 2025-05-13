using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync(int userId);
        Task<bool> CompleteOrderAsync(string claimCode, int adminUserId);

    }
}
