using HDV.BTL.Auth.Dto;
using HDV.BTL.Auth.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            var userDto = await _userService.CreateAsync(createUserDto);
            return userDto;
        }

        [HttpPost("auth")]
        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            return null;
        }
    }
}
