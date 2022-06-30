using HDV.Nhom2.AuthService.DL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.AuthService.BL
{
    public class AuthServiceBL: IAuthServiceBL
    {
        #region Khởi tạo

        private string _connectionString;

        private readonly IOptions<JwtSetting> _jwtSettings;

        public AuthServiceBL(IConfiguration configuration,
            IOptions<JwtSetting> jwtSettings)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Hàm
        public async Task<User> CreateUserAsync(CreateUserReqDto createUserReqDto)
        {
            using var authServiceDL = new AuthServiceDL(_connectionString);

            // kiểm tra email đã tồn tại chưa
            var existUser = await authServiceDL.GetUserByEmail(createUserReqDto.Email);

            if(existUser != null)
            {
                throw new Nhom2Exception("E1000", "Email đã tồn tại");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = createUserReqDto.Email,
                PasswordSalt = Guid.NewGuid().ToString(),
                FirstName = createUserReqDto.FirstName,
                LastName = createUserReqDto.LastName,
                CreatedDate = DateTime.Now
            };

            user.PasswordHash = CreateMD5($"{createUserReqDto.Password}{user.PasswordSalt}");

            
            var newUser = await authServiceDL.CreateUserAsync(user);

            return newUser;
        }

        public async Task<AuthenticateResDto> LoginAsync(AuthenticateReqDto authenticateReqDto)
        {
            using var authServiceDL = new AuthServiceDL(_connectionString);

            var user = await authServiceDL.GetUserByEmail(authenticateReqDto.Email);

            if(user == null)
            {
                throw new Nhom2Exception("E1000", "Email không đúng");
            }

            var passwordHash = CreateMD5($"{authenticateReqDto.Password}{user.PasswordSalt}");
            if(!passwordHash.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase))
            {
                throw new Nhom2Exception("E1001", "Mật khẩu không đúng");
            }

            string securityKey = _jwtSettings.Value.SecretKey;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
            );

            var authenticateResDto = new AuthenticateResDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = authenticateReqDto.Email
            };

            return authenticateResDto;
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
        #endregion
    }
}
