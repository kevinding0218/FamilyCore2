using System.Threading.Tasks;

namespace WebApi.EmailSettings
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
