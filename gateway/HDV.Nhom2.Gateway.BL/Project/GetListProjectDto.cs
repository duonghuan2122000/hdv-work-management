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
        [JsonProperty("total_count")]
        public int TotalCount { get; set; } = 0;

        [JsonProperty("items")]
        public List<T> Items { get; set; } = new List<T>();
    }

    public class ProjectDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("status")]
        public string StatusStr { private get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public int Status
        {
            get
            {
                switch (StatusStr.ToLower())
                {
                    case "on working":
                        return 1;

                    case "finish":
                        return 2;

                    case "not finish":
                        return 3;

                    default:
                        return 1;
                }
            }
        }
    }
}
