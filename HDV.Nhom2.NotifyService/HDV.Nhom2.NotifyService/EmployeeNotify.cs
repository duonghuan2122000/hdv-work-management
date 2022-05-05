namespace HDV.Nhom2.NotifyService
{
    public class EmployeeNotify
    {
        public Guid Id { get; set; }

        public int CompanyId { get; set; }

        public int EmployeeId { get; set; }

        public string EmailCc { get; set; }
    }
}
