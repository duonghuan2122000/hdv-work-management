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
    public class TaskService: ITaskService
    {
        private readonly IOptions<ProjectNetServiceOption> _projectNetServiceOptions;

        private readonly ICallService _callService;

        public TaskService(IOptions<ProjectNetServiceOption> projectNetServiceOptions,
            ICallService callService)
        {
            _projectNetServiceOptions = projectNetServiceOptions;
            _callService = callService;
        }


        public async Task<GetListTaskDto<TaskDto>> GetList(int employeeId)
        {
            var url = $"{_projectNetServiceOptions.Value.BaseUrl}/tasks/list";

            var getListTaskResponse = await _callService.CallRestApiAsync(url, "POST", new
            {
                employeeId
            });

            if (getListTaskResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }

            var getListTaskRes = JsonConvert.DeserializeObject<GetListTaskDto<TaskDto>>(getListTaskResponse.JsonObject);

            return getListTaskRes;
        }
    }
}
