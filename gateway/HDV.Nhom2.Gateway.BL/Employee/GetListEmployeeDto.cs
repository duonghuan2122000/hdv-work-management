using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    /// <summary>
    /// Dto thông tin nhân viên từ service go
    /// </summary>
    /// CreatedBy: dbhuan 25/06/2022
    public class EmployeeGoDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("dob")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string GenderStr { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        public int Gender
        {
            get
            {
                return GenderStr switch
                {
                    "male" => 1,
                    "female" => 2,
                    _ => 3,
                };
            }
        }
    }

    public class EmployeeDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string GenderStr { get; set; }

        public string Role { get; set; }

        public int Gender { get; set; }
    }

    public class GetListEmployeeDto<T>
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        public GetListEmployeeDto()
        {
            TotalCount = 0;
            Items = new List<T>();
        }
    }
}
