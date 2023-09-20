using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using static Dapper.SqlMapper;

namespace MISA.AspNetCore.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbConnectionService dbConnectionService) : base(dbConnectionService)
        {
        }

        public async Task<bool> CheckDuplicateEmployeeCodeAsync(string employeeCode)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Code = @employeeCode";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@employeeCode", employeeCode);

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleAsync<Employee>(sql, parameters);

            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
