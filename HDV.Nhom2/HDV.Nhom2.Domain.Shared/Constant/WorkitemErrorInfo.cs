using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain.Shared
{
    public class CreateWorkitemErrorInfo
    {
        public class Code
        {
            public const string ProjectNotFound = "E1000";

            public const string EmployeeNotFound = "E1001";
        }

        public class Message
        {
            public const string ProjectNotFound = "Dự án không tồn tại";

            public const string EmployeeNotFound = "Nhân viên không tồn tại";
        }
    }
}
