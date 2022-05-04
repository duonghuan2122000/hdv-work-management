using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain.Shared
{
    public class CreateProjectErrorInfo
    {
        public class Code
        {
            public const string CompanyNotFound = "E1000";
        }

        public class Message
        {
            public const string CompanyNotFound = "Công ty không tồn tại";
        }
    }
}
