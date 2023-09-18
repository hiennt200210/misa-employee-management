using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Infrastructure
{
    public class DbConnectionService : IDbConnectionService
    {
        private readonly string _connectionString;

        public DbConnectionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy thông tin kết nối cơ sở dữ liệu
        /// </summary>
        /// <returns>Kết nối cơ sở dữ liệu</returns>
        /// CreatedBy: hiennt200210 (18/09/2023)
        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);

            return connection;
        }
    }
}
