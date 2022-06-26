using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class GetListTaskDto<T>
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; } = 0;

        [JsonProperty("items")]
        public List<T> Items { get; set; } = new List<T>();
    }

    public class TaskDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string StatusStr { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        public int Status
        {
            get
            {
                return StatusStr.ToLower() switch
                {
                    "on working" => 1,
                    "finish" => 2,
                    "not finish" => 3,
                    _ => 1,
                };
            }
        }
    }
}
