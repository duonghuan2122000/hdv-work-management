using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Project.HttpApi
{
    public class GetListTaskDto
    {
        public List<TaskDto> Items { get; set; }

        public int TotalCount { get; set; }
    }

    public class TaskDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string EmployeeId { get; set; }

        public string ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class GetListTaskFilterDto
    {
        public int? EmployeeId { get; set; }
    }
}
