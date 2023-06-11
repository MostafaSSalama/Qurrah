using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace Qurrah.Business.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IConfiguration config)
        {
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}