using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Domain;
using HDV.Nhom2.Domain.Shared;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthRequiredAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var existAuthHeader = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizationHeader);
            if (StringValues.IsNullOrEmpty(authorizationHeader))
            {
                throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
            }

            var authorization = authorizationHeader.FirstOrDefault();

            if (string.IsNullOrEmpty(authorization))
            {
                throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
            }

            string[] tokenArr = authorization.Split(" ");
            if(tokenArr.Length != 2 || tokenArr[0] == "Bearer")
            {
                throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
            }

            var token = tokenArr[1];

            try
            {
                IOptions<JwtSetting> jwtSettings = (IOptions<JwtSetting>)context.HttpContext.RequestServices.GetService(typeof(IOptions<JwtSetting>));

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtSettings.Value.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Value.Issuer,
                    ValidAudience = jwtSettings.Value.Issuer,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var employeeIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "EmployeeId");
                if(employeeIdClaim == null)
                {
                    throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
                }

                var employeeId = Guid.Parse(employeeIdClaim.Value);

                IEmployeeRepository employeeRepository = (IEmployeeRepository)context.HttpContext.RequestServices.GetService(typeof(IEmployeeRepository));

                var employee = await employeeRepository.GetAsync(employeeId);

                if(employee == null)
                {
                    throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                if(ex is Nhom2Exception)
                {
                    throw;
                }
                throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
            }


        }
    }
}
