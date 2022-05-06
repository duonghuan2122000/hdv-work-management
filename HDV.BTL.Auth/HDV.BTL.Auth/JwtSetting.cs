using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth
{
    public class JwtSetting
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }
    }
}
