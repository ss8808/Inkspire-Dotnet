using Inksprie_Backend.Data;
using Inksprie_Backend.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Inksprie_Backend.Services
{
    public class EmailService: IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public EmailService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task SendClaimCodeEmailAsync(int userId, string claimCode, decimal total)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
                EnableSsl = true
            };

            var mail = new MailMessage(_config["Email:Username"], user.Email)
            {
                Subject = "Book Order Confirmation - Inkspire",
                Body = $"Thank you for your order.\n\nClaim Code: {claimCode}\nTotal: ${total:F2}\n\nPresent this at the store to claim your order.",
                IsBodyHtml = false
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
