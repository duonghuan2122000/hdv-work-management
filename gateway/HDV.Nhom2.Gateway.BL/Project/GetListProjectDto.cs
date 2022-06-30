using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class GetListProjectDto<T>
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; } = 0;

        [JsonProperty("items")]
        public List<T> Items { get; set; } = new List<T>();
    }

    public class ProjectDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
