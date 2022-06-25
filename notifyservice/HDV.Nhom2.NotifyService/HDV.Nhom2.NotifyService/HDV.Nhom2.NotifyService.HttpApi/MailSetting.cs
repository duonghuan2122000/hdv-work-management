using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.NotifyService.HttpApi
{
    /// <summary>
    /// Thông số cấu hình gửi mail
    /// </summary>
    /// CreatedBy: dbhuan 29/05/2022
    public class MailSetting
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
