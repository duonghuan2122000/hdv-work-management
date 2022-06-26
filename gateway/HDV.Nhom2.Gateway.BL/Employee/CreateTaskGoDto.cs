using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class CreateTaskGoReqDto
    {
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = "on working";

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class CreateTaskGoResDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
