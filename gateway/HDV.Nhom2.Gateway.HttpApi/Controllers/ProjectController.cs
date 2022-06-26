using HDV.Nhom2.Gateway.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.HttpApi.Controllers
{
    /// <summary>
    /// Controller project
    /// </summary>
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("list")]
        public async Task<GetListProjectDto<ProjectDto>> GetList()
        {
            var res = await _projectService.GetList();
            return res;
        }
    }
}
