using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain.Shared
{
    public class CreateEmployeeErrorInfo
    {
        public class Code
        {
            public const string EmailOrUserNameExist = "E1000";
        }

        public class Message
        {
            public const string EmailOrUsernameExist = "Email hoặc tên đăng nhập đã tồn tại";
        }
    }

    public class AuthEmployeeErrorInfo
    {
        public class Code
        {
            public const string UsernameInvalid = "E1000";

            public const string PasswordInvalid = "E1001";
        }

        public class Message
        {
            public const string UsernameInvalid = "Email hoặc tên đăng nhập không hợp lệ";

            public const string PasswordInvalid = "Mật khẩu không hợp lệ";

        }
    }
}
