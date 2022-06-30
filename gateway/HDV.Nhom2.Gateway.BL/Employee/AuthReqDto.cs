using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class AuthReqDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class AuthResDto
    {
        public string Id { get; set; }
        public string Token { get; set; }

        public string Email { get; set; }

        //public int Gender { get; set; }

        public string Role { get; set; }

        public int CompanyId { get; set; }
    }
}
