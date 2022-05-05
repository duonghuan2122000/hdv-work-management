using System.Net;

namespace HDV.Nhom2.NotifyService
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
