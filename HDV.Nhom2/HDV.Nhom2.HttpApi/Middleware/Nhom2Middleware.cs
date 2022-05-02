using HDV.Nhom2.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi
{
    public class Nhom2Middleware
    {
        private readonly RequestDelegate _next;

        public Nhom2Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            Log.Logger.Error("Nhom2Middleware-HandleException-Exception: {ex}", ex);
            string errorCode = ErrorInfo.Code.InternalServerError;
            string errorMessage = ErrorInfo.Message.InternalServerError;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if(ex is Nhom2Exception)
            {
                var nhom2Exception = (Nhom2Exception)ex;
                errorCode = nhom2Exception.ErrorCode;
                errorMessage = nhom2Exception.ErrorMessage;
                statusCode = nhom2Exception.StatusCode;
            }

            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            }));
        }
    }
}
