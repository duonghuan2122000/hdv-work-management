using HDV.BTL.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Service
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);

        Task<string> Login(LoginUserDto loginUserDto);
    }
}
