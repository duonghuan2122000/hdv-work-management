namespace HDV.Nhom2.EmailService
{
    /// <summary>
    /// Req gửi mail
    /// </summary>
    /// CreatedBy: dbhuan 04/05/2022
    public class MailContentDto
    {
        public string To { get; set; }

        public string Cc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
