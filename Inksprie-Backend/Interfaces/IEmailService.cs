namespace Inksprie_Backend.Interfaces
{
    public interface IEmailService
    {
        Task SendClaimCodeEmailAsync(int userId, string claimCode, decimal total);
    }
}
