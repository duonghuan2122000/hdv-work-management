using HDV.Nhom2.Application.Contracts;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IOptions<MailSetting> _mailSetting;
        public NotificationController(IOptions<MailSetting> mailSettings)
        {
            _mailSetting = mailSettings;
        }

        [HttpPost("mail")]
        public async Task SendMail(MailContentReq mailContentReq)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSetting.Value.DisplayName, _mailSetting.Value.Mail);
            email.From.Add(new MailboxAddress(_mailSetting.Value.DisplayName, _mailSetting.Value.Mail));
            email.To.Add(MailboxAddress.Parse(mailContentReq.To));
            email.Subject = mailContentReq.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContentReq.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_mailSetting.Value.Host, _mailSetting.Value.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSetting.Value.Mail, _mailSetting.Value.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                //System.IO.Directory.CreateDirectory("mailssave");
                //var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                //await email.WriteToAsync(emailsavefile);
            }

            smtp.Disconnect(true);
        }
    }
}
