using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth
{
    public class Nhom2DbContext
    {
        private MySqlConnection _conn;

        public Nhom2DbContext(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Default"];
            _conn = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return _conn;
        }
    }
}
