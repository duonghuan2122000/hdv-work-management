using HDV.Nhom2.AuthService.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.BL
{
    public interface IAuthServiceBL
    {
        Task<User> CreateUserAsync(CreateUserReqDto createUserReqDto);

        Task<AuthenticateResDto> LoginAsync(AuthenticateReqDto authenticateReqDto);

    }
}
