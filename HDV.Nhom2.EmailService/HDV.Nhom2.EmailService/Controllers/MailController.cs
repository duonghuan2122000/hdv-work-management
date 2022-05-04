using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;

namespace HDV.Nhom2.EmailService.Controllers
{
    [Route("mails")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IOptions<EmailSetting> _emailSettings;

        public MailController(IOptions<EmailSetting> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        [HttpPost("send")]
        public async Task SendMailAsync(MailContentDto mailContent)
        {
            Log.Logger.Debug("MailController-SendMailAsync-Req: {@mailContent}", mailContent);
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_emailSettings.Value.DisplayName, _emailSettings.Value.Mail);
            email.From.Add(new MailboxAddress(_emailSettings.Value.DisplayName, _emailSettings.Value.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Cc.Add(MailboxAddress.Parse(mailContent.Cc));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_emailSettings.Value.Host, _emailSettings.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Value.Mail, _emailSettings.Value.Password);
                await smtp.SendAsync(email);
                Log.Logger.Debug("MailController-SendMailAsync-Email: {@email}", email);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("MailController-SendMailAsync-Exception: {ex}", ex);
            }
        }
    }
}
