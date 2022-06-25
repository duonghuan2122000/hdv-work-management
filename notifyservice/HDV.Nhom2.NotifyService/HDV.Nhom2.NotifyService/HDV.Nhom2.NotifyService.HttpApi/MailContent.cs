using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.NotifyService.HttpApi
{
    /// <summary>
    /// Req gửi mail
    /// </summary>
    /// CreatedBy: dbhuan 29/04/2022
    public class MailContent
    {
        /// <summary>
        /// Người nhận
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Chủ đề mail
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Nội dung mail
        /// </summary>
        public string Body { get; set; }
    }
}
