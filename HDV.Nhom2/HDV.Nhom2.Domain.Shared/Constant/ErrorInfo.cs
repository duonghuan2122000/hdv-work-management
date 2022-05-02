using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain.Shared
{
    public class ErrorInfo
    {
        public class Code
        {
            public const string UnAuthorized = "E2000";
            public const string InternalServerError = "E3000";
        }

        public class Message
        {
            public const string UnAuthorized = "Không có quyền hạn truy cập";

            public const string InternalServerError = "Lỗi hệ thống";
        }
    }
}
