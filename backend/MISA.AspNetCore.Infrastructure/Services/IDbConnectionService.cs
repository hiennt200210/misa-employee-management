using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Infrastructure
{
    public interface IDbConnectionService
    {
        /// <summary>
        /// Lấy thông tin kết nối tới cơ sở dữ liệu
        /// </summary>
        /// <returns>Thông tin kết nối cơ sở dữ liệu</returns>
        /// CreatedBy: hiennt200210 (18/09/2023)
        MySqlConnection GetConnection();
    }
}
