using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using static Dapper.SqlMapper;
using System.Resources;

namespace MISA.AspNetCore.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbConnectionService dbConnectionService) : base(dbConnectionService)
        {
        }

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <exception cref="ConflictException">Mã nhân viên đã tồn tại</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task CheckDuplicateEmployeeCodeAsync(string employeeCode)
        {
            // Tạo kết nối với database
            var connection = base.DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT EmployeeCode FROM {base.TableName} WHERE {base.TableName}Code = @employeeCode";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@employeeCode", employeeCode);

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleOrDefaultAsync<Employee>(sql, parameters);

            if (result != null)
            {
                throw new ConflictException()
                {
                    DevMessage = Domain.Resources.Errors.Conflict,
                    UserMessage = Domain.Resources.Errors.Conflict,
                    Errors = Domain.Resources.Errors.EmployeeCodeDuplicated
                };
            }
        }
    }
}
