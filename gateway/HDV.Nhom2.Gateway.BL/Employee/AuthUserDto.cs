using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class AuthUserReqDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class AuthUserResDto
    {
        public string Token { get; set; }

        public string Email { get; set; }
    }

    public class ErrorObj
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
