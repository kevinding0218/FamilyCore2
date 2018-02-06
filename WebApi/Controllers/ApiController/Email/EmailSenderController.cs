using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.EmailSettings;

namespace WebApi.Controllers.ApiController.Email
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/emailsender")]
    public class EmailSenderController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailSenderController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("emailTest")]
        public async Task TestAction()
        {
            await _emailSender.SendEmailAsync("kevinding0218@gmail.com", "Email Sender Test",
                         $"To Test If Email Working");
        }
    }
}