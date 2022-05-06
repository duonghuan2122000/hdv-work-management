using HDV.BTL.Auth.Dto;
using HDV.BTL.Auth.Entity;
using HDV.BTL.Auth.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        private readonly IOptions<JwtSetting> _jwtSetting;

        public UserService(IUserRepository userRepo, IOptions<JwtSetting> jwtSetting)
        {
            _userRepo = userRepo;
            _jwtSetting = jwtSetting;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            var existUser = await _userRepo.GetAsync(createUserDto.Email);
            if(existUser != null)
            {
                throw new Exception("Đã tồn tại email");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Email = createUserDto.Email,
                PasswordSalt = Guid.NewGuid().ToString()
            };

            user.Password = CreateMD5($"{createUserDto.Password}|{user.PasswordSalt}");

            // gọi api tạo nhân viên
            var urlCreateEmployee = $"http://dbhuan.tk:5012/company/{createUserDto.CompanyId}/employee";
            var createEmployeeReqDto = new CreateEmployeeReqDto
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Dob = createUserDto.Dob,
                Gender = createUserDto.Gender,
                Role = createUserDto.Role
            };

            var createEmployeeClient = new RestClient(urlCreateEmployee);
            var createEmployeeRequest = new RestRequest("", Method.Post);
            createEmployeeRequest.AddJsonBody(createEmployeeReqDto);
            var createEmployeeResponse = await createEmployeeClient.ExecuteAsync(createEmployeeRequest);

            if (!createEmployeeResponse.IsSuccessful)
            {
                throw new Exception("Lỗi hệ thống");
            }

            var createEmployeeResDto = JsonConvert.DeserializeObject<CreateEmployeeResDto>(createEmployeeResponse.Content);

            user.EmployeeId = int.Parse(createEmployeeResDto.Id);

            var newUser = await _userRepo.CreateAsync(user);
            var userDto = new UserDto
            {
                Id = newUser.Id,
                Email = newUser.Email,
                CreatedDate = newUser.CreatedDate
            };

            return userDto;
        }

        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            var user = await _userRepo.GetAsync(loginUserDto.Email);

            if (user == null)
                throw new Exception("UnAuthorized");

            if(!user.Password.Equals(CreateMD5($"{loginUserDto.Password}|{user.PasswordSalt}")))
                throw new Exception("UnAuthorized");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Value.Secret));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtSetting.Value.Issuer, null, null, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToUpper(); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
