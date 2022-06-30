using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.BL
{
    public class AuthenticateReqDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class AuthenticateResDto
    {
        public string Token { get; set; }

        public string Email { get; set; }
    }
}
