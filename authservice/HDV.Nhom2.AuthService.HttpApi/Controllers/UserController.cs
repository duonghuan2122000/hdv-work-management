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

        private readonly IAuthServiceBL _authServiceBL;

        public UserController(IAuthServiceBL authServiceBL)
        {
            _authServiceBL = authServiceBL;    
        }
        #endregion

        #region Hàm
        [HttpPost]
        public async Task<User> CreateUserAsync(CreateUserReqDto createUserReqDto)
        {
            Log.Logger.Debug("UserController-CreateUserAsync-Req: {@createUserReqDto}", createUserReqDto);
            var newUser = await _authServiceBL.CreateUserAsync(createUserReqDto);
            Log.Logger.Debug("UserController-CreateUserAsync-Res: {@newUser}", newUser);
            return newUser;
        }

        [HttpPost("authenticate")]
        public async Task<AuthenticateResDto> LoginAsync(AuthenticateReqDto authenticateReqDto)
        {
            var res = await _authServiceBL.LoginAsync(authenticateReqDto);
            return res;
        }
        #endregion
    }
}
