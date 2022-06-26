using HDV.Nhom2.Infrastructure.Contracts.CallService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class ProjectService: IProjectService
    {
        private readonly ICallService _callService;

        private readonly IOptions<ProjectServiceOption> _projectServiceOptions;

        public ProjectService(ICallService callService,
            IOptions<ProjectServiceOption> projectServiceOptions)
        {
            _callService = callService;
            _projectServiceOptions = projectServiceOptions;
        }

        public async Task<GetListProjectDto<ProjectDto>> GetList()
        {
            var url = $"{_projectServiceOptions.Value.BaseUrl}/projects?page=1&limit=100000";

            var getListProjectResponse = await _callService.CallRestApiAsync(url, "GET", null);

            if(getListProjectResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }

            var getListProjectRes = JsonConvert.DeserializeObject<GetListProjectDto<ProjectDto>>(getListProjectResponse.JsonObject);

            return getListProjectRes;
        }
    }
}
