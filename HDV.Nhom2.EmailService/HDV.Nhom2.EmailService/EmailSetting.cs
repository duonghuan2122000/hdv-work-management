namespace HDV.Nhom2.EmailService
{
    /// <summary>
    /// Thông số cấu hình cho mail gửi đi
    /// </summary>
    public class EmailSetting
    {
        public string Mail { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
