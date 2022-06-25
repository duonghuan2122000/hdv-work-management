using Confluent.Kafka;
using HDV.Nhom2.Gateway.BL;
using HDV.Nhom2.Infrastructure.Contracts.CallService;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.HttpApi.Controllers
{
    [Route("employeess")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="createEmployeeReqDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateEmployeeReqDto createEmployeeReqDto)
        {
            Log.Logger.Debug("EmployeeController-CreateAsync-Req: {@createEmployeeReqDto}", createEmployeeReqDto);
            var createEmployeeResDto = await _employeeService.CreateAsync(createEmployeeReqDto);
            Log.Logger.Debug("EmployeeController-CreateAsync-Res: {@createEmployeeResDto}", createEmployeeResDto);
            return Ok(createEmployeeResDto);
        }
    }
}
