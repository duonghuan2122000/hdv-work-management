using HDV.Nhom2.AuthService.BL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.HttpApi
{
    public class Nhom2Middleware
    {
        private readonly RequestDelegate _next;

        public Nhom2Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
            context.Response.Headers.Add("Content-Type", "application/json");
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            }));
        }
    }
}
