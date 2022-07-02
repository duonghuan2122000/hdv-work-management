using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.NotifyService.HttpApi.Controllers
{
    /// <summary>
    /// Controller thông báo
    /// </summary>
    [Route("notify")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        #region Khởi tạo

        /// <summary>
        /// cấu hình gửi mail
        /// </summary>
        private readonly IOptions<MailSetting> _mailSettings;

        public NotifyController(IOptions<MailSetting> mailSettings)
        {
            _mailSettings = mailSettings;
        }
        #endregion

        #region Hàm

        [HttpPost("mail")]
        public async Task SendMail(MailContent mailContent)
        {
            Log.Logger.Debug("NotifyController-SendMail-Req: {@mailContent}", mailContent);
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.Value.DisplayName, _mailSettings.Value.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.Value.DisplayName, _mailSettings.Value.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.Email));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Value.Mail, _mailSettings.Value.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                //System.IO.Directory.CreateDirectory("mailssave");
                //var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                //await email.WriteToAsync(emailsavefile);
                Log.Logger.Error("NotifyController-SendMail-Exception: {ex}", ex);

                //logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                //logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            //logger.LogInformation("send mail to " + mailContent.To);
        }
        #endregion
    }
}
