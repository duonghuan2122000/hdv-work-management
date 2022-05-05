using MySql.Data.MySqlClient;
using System.Configuration;

namespace HDV.Nhom2.NotifyService.Data
{
    public class NotifyDbContext
    {
        private MySqlConnection _conn;

        public NotifyDbContext(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return _conn;
        }
    }
}
