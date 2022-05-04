using HDV.Nhom2.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class WorkitemController : ControllerBase
    {
        private readonly IWorkitemService _workitemService;

        public WorkitemController(IWorkitemService workitemService)
        {
            _workitemService = workitemService;
        }

        [HttpPost]
        public async Task<CreateWorkitemRes> CreateAsync(CreateWorkitemReq createWorkitemReq)
        {
            string token = Request.Headers["Authorization"];
            var createWorkitemRes = await _workitemService.CreateAsync(createWorkitemReq, token);
            return createWorkitemRes;
        }
    }
}
