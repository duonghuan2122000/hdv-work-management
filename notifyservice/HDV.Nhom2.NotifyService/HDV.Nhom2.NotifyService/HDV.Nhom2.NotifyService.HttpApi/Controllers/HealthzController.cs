using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.NotifyService.HttpApi.Controllers
{
    [Route("healthz")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        [HttpGet]
        public IActionResult CheckHealthz()
        {
            return Ok();
        }
    }
}
