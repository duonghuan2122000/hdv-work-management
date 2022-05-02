using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public class JwtSetting
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }
    }
}
