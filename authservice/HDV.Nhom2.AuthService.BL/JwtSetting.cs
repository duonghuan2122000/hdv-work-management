using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.BL
{
    public class JwtSetting
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }
    }
}
