using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.BL
{
    public class Nhom2Exception: Exception
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public Nhom2Exception(string errorCode, string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
