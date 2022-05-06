using Dapper;
using HDV.BTL.Auth.Entity;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Repository
{
    public class UserRepository : IUserRepository
    {
        private MySqlConnection _conn;
        public UserRepository(IConfiguration configuration)
        {
            _conn = new Nhom2DbContext(configuration).GetConnection();
        }

        public async Task<User> CreateAsync(User user)
        {
            var createUserSql = "insert into user (Id, Email, Password, PasswordSalt, CreatedDate, EmployeeId) values (@Id, @Email, @Password, @PasswordSalt, @CreatedDate, @EmployeeId)";
            await _conn.OpenAsync();

            await _conn.ExecuteAsync(createUserSql, new
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,
                PasswordSalt = user.PasswordSalt,
                CreatedDate = user.CreatedDate,
                EmployeeId = user.EmployeeId
            });

            await _conn.CloseAsync();

            return user;
        }

        public async Task<User> GetAsync(string email)
        {
            var getUserSql = "select * from user where email = @Email";
            await _conn.OpenAsync();
            var user = await _conn.QueryFirstOrDefaultAsync<User>(getUserSql, new
            {
                Email = email
            });
            await _conn.CloseAsync();
            return user;

        }
    }
}
