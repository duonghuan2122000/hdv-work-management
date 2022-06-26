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
    [Route("employees")]
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

        /// <summary>
        /// Lấy danh sách nhân viên bằng email hoặc tên
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetListEmployee([FromQuery] string keyword)
        {
            Log.Logger.Debug("EmployeeController-GetListEmployee");
            var res = await _employeeService.GetListEmployee(keyword);
            return Ok(res);
        }

        /// <summary>
        /// Tạo và giao task cho nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 25/06/2022
        /// <param name="createAndAssignTaskForEmployeeReqDto"></param>
        /// <returns></returns>
        [HttpPost("assign-task")]
        public async Task<bool> CreateAndAssignTaskForEmployee(CreateAndAssignTaskForEmployeeReqDto createAndAssignTaskForEmployeeReqDto)
        {
            var res = await _employeeService.CreateAndAssignTaskForEmployee(createAndAssignTaskForEmployeeReqDto);
            return res;
        }
    }
}
