namespace GeletoCarDealer.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(
            string email,
            string subject,
            string message);
    }
}
