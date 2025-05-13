using Inksprie_Backend.Dtos;

namespace Inksprie_Backend.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync(int userId);
        Task<bool> CompleteOrderAsync(string claimCode, int adminUserId);
        Task<IEnumerable<OrderHistoryDto>> GetAllOrderHistoryAsync();
        Task<IEnumerable<OrderHistoryDto>> GetUserOrderHistoryAsync(int userId);
        Task<bool> CancelOrderAsync(string claimCode, int userId);


    }
}
