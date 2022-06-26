using HDV.Nhom2.AuthService.BL;
using HDV.Nhom2.AuthService.DL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.HttpApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Khởi tạo
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        #endregion

        #region Hàm
        [HttpPost]
        public async Task<User> CreateUserAsync(CreateUserReqDto createUserReqDto)
        {
            Log.Logger.Debug("UserController-CreateUserAsync-Req: {@createUserReqDto}", createUserReqDto);
            var authServiceBL = new AuthServiceBL(_connectionString);
            var newUser = await authServiceBL.CreateUserAsync(createUserReqDto);
            Log.Logger.Debug("UserController-CreateUserAsync-Res: {@newUser}", newUser);
            return newUser;
        }

        [HttpPost("authenticate")]
        public async Task<AuthenticateResDto> LoginAsync(AuthenticateReqDto authenticateReqDto)
        {
            var authServiceBL = new AuthServiceBL(_connectionString);
            var res = await authServiceBL.LoginAsync(authenticateReqDto);
            return res;
        }
        #endregion
    }
}
