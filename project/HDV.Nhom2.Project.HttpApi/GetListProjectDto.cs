using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Project.HttpApi
{
    public class ProjectDto
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class GetListProjectDto
    {
        public int TotalCount { get; set; } = 0;

        public List<ProjectDto> Items { get; set; } = new List<ProjectDto>();
    }

    public class GetListFilterProjectDto
    {
        public int? CompanyId { get; set; }
    }
}
