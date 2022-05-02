using HDV.Nhom2.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi.Controllers
{
    [Route("employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Khởi tạo
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Hàm
        /// <summary>
        /// Tạo mới nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 02/05/2022
        /// <param name="createEmployeeReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CreateEmployeeRes> CreateAsync(CreateEmployeeReq createEmployeeReq)
        {
            var createEmployeeRes = await _employeeService.CreateAsync(createEmployeeReq);
            return createEmployeeRes;
        }

        /// <summary>
        /// Xác thực nhân viên
        /// </summary>
        /// <param name="authEmployeeReq"></param>
        /// <returns></returns>
        [HttpPost("auth")]
        public async Task<AuthEmployeeRes> AuthEmployee(AuthEmployeeReq authEmployeeReq)
        {
            var authEmployeeRes = await _employeeService.AuthEmployee(authEmployeeReq);
            return authEmployeeRes;
        }
        #endregion
    }
}
