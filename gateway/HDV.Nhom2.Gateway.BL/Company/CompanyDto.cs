using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    /// <summary>
    /// Thông tin công ty
    /// </summary>
    /// CreatedBy: dbhuan 19/06/2022
    public class CompanyDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [JsonProperty("tax_number")]
        public string TaxNumber { get; set; }

        [JsonProperty("total_employee")]
        public int TotalEmployee { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedDate { get; set; }
    }
}
