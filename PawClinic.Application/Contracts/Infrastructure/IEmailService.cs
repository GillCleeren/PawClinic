using PawClinic.Application.Models.Mail;

namespace PawClinic.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
