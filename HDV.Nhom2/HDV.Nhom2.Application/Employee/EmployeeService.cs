using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Domain;
using HDV.Nhom2.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application
{
    public class EmployeeService: IEmployeeService
    {
        #region Khởi tạo

        private readonly IEmployeeRepository _employeeRepository;

        private readonly ICommonUtility _commonUtility;

        private readonly IOptions<JwtSetting> _jwtSettings;

        public EmployeeService(IEmployeeRepository employeeRepository, ICommonUtility commonUtility, IOptions<JwtSetting> jwtSettings)
        {
            _employeeRepository = employeeRepository;
            _commonUtility = commonUtility;
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Hàm

        /// <summary>
        /// Tạo mới nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 02/05/2022
        /// <param name="createEmployeeReq"></param>
        /// <returns></returns>
        public async Task<CreateEmployeeRes> CreateAsync(CreateEmployeeReq createEmployeeReq)
        {
            var employee = new Employee
            {
                FirstName = createEmployeeReq.FirstName,
                LastName = createEmployeeReq.LastName,
                Email = createEmployeeReq.Email,
                Username = createEmployeeReq.Username,
                PasswordSalt = Guid.NewGuid().ToString()
            };

            employee.PasswordHash = _commonUtility.Md5(createEmployeeReq.Password + employee.PasswordSalt);

            var newEmployee = await _employeeRepository.CreateAsync(employee);

            return new CreateEmployeeRes
            {
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Email = newEmployee.Email,
                Username = newEmployee.Username
            };
        }

        /// <summary>
        /// Xác thực nhân viên
        /// </summary>
        /// <param name="authEmployeeReq"></param>
        /// <returns></returns>
        public async Task<AuthEmployeeRes> AuthEmployee(AuthEmployeeReq authEmployeeReq)
        {
            var employee = await _employeeRepository.GetAsync(authEmployeeReq.Username, authEmployeeReq.Username);
            if(employee == null)
            {
                throw new Nhom2Exception(AuthEmployeeErrorInfo.Code.UsernameInvalid, AuthEmployeeErrorInfo.Message.UsernameInvalid);
            }

            if(!employee.PasswordHash.Equals(_commonUtility.Md5(authEmployeeReq.Password + employee.PasswordSalt), StringComparison.OrdinalIgnoreCase))
            {
                throw new Nhom2Exception(AuthEmployeeErrorInfo.Code.PasswordInvalid, AuthEmployeeErrorInfo.Message.PasswordInvalid);
            }

            var expireDate = DateTime.Now.AddDays(1);

            var claims = new Claim[]
            {
                new Claim("EmployeeId", employee.Id.ToString()),
                new Claim("ExpireDate", expireDate.ToString("yyyyMMddHHmmss")),
                new Claim("Scope", "Nhom2")
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret));
            var signIn = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _jwtSettings.Value.Issuer, 
                _jwtSettings.Value.Issuer, 
                claims, 
                expires: expireDate, 
                signingCredentials: signIn);

            return new AuthEmployeeRes
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireTime = 86400
            };
        }
        #endregion
    }
}
