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

        private readonly IOptions<ProjectNetServiceOption> _projectNetServiceOptions;

        public ProjectService(ICallService callService,
            IOptions<ProjectServiceOption> projectServiceOptions, 
            IOptions<ProjectNetServiceOption> projectNetServiceOptions)
        {
            _callService = callService;
            _projectServiceOptions = projectServiceOptions;
            _projectNetServiceOptions = projectNetServiceOptions;
        }

        public async Task<GetListProjectDto<ProjectDto>> GetList(int companyId)
        {
            var url = $"{_projectNetServiceOptions.Value.BaseUrl}/projects/list";

            var getListProjectResponse = await _callService.CallRestApiAsync(url, "POST", new 
            {
                companyId
            });

            if(getListProjectResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }

            var getListProjectRes = JsonConvert.DeserializeObject<GetListProjectDto<ProjectDto>>(getListProjectResponse.JsonObject);

            return getListProjectRes;
        }
    }
}
