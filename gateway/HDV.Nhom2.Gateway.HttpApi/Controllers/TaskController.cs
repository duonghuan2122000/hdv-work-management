using HDV.Nhom2.Gateway.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.HttpApi.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("list")]
        public async Task<GetListTaskDto<TaskDto>> GetList([FromQuery] int employeeId)
        {
            var res = await _taskService.GetList(employeeId);
            return res;
        }
    }
}
