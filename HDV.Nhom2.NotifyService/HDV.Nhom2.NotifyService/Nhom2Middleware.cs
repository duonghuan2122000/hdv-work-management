using Newtonsoft.Json;
using System.Net;

namespace HDV.Nhom2.NotifyService
{
    public class Nhom2Middleware
    {
        private readonly RequestDelegate _requestDelegate;

        public Nhom2Middleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            string errorCode = "E3000";
            string errorMessage = "Lỗi hệ thống";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if(ex is Nhom2Exception)
            {
                var nhom2Ex = (Nhom2Exception)ex;
                errorCode = nhom2Ex.ErrorCode;  
                errorMessage = nhom2Ex.ErrorMessage;    
                statusCode = nhom2Ex.StatusCode;    
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
            }));
        }
    }
}
