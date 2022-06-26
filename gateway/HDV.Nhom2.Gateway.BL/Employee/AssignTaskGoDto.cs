using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class AssignTaskGoReqDto
    {
        [JsonProperty("employee_id")]
        public string EmployeeId { get; set; }

        [JsonProperty("task_id")]
        public string TaskId { get; set; }
    }
}
