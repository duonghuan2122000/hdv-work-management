using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Serilog;

namespace HDV.Nhom2.AuthService.DL
{
    public class AuthServiceDL: IDisposable
    {
        #region Khởi tạo

        private readonly MySqlConnection _conn;
        public AuthServiceDL(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }
        #endregion

        #region Hàm
        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                //await _conn.OpenAsync();
                await _conn.ExecuteAsync(
                    "INSERT INTO user (Id, Email, PasswordHash, PasswordSalt, FirstName, LastName, CreatedDate) VALUES (@Id, @Email, @PasswordHash, @PasswordSalt, @FirstName, @LastName, @CreatedDate)",
                    user
                );

                return user;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("AuthServiceDL-CreateUserAsync-Exception: {ex}", ex);
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = null;
            try
            {
                //await _conn.OpenAsync();
                user = await _conn.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM user WHERE Email = @Email LIMIT 1",
                    new
                    {
                        Email = email
                    }
                );
            }
            catch (Exception ex)
            {
                Log.Logger.Error("AuthServiceDL-GetUserByEmail-Exception: {ex}", ex);
            }
            Log.Logger.Debug("AuthServiceDL-GetUserByEmail-User: {@user}", user);
            return user;
        }

        public void Dispose()
        {
            _conn.Close();
        }
        #endregion
    }
}
