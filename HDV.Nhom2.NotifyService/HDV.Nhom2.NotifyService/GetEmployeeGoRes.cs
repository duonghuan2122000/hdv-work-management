using Newtonsoft.Json;

namespace HDV.Nhom2.NotifyService
{
    public class GetEmployeeGoRes
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "company_id")]
        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string Role { get; set; }
    }
}
