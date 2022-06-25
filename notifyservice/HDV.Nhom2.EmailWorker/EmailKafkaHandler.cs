using HDV.Nhom2.Infrastructure.Contracts.CallService;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.EmailWorker
{
    public class EmailKafkaHandler<Null, MailContent>: IKafkaHandler<Null, MailContent>
    {
        private readonly ICallService _callService;
        private readonly IOptions<EmailServiceOption> _emailServiceOptions;
        public EmailKafkaHandler(ICallService callService, IOptions<EmailServiceOption> emailServiceOptions)
        {
            _callService = callService;
            _emailServiceOptions = emailServiceOptions;
        }

        /// <summary>
        /// Xử lý msg từ queue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task HandlerAsync(Null key, MailContent mailContent)
        {
            // gọi service gửi mail
            var sendMailUrl = $"{_emailServiceOptions.Value.BaseUrl}/notify/mail";

            var sendMailResponse = await _callService.CallRestApiAsync(sendMailUrl, "post", mailContent);
            Log.Logger.Debug("EmailKafkaHandler-HandlerAsync-CallService: url={sendMailUrl} - Res={@sendMailResponse}", sendMailUrl, sendMailResponse);
        }
    }
}
