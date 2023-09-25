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

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            // Tạo kết nối với database
            var connection = base.DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT {base.TableName}Code FROM {base.TableName} ORDER BY {base.TableName}Code DESC LIMIT 1";

            // Thực thi câu truy vấn
            var result = await connection.QueryFirstOrDefaultAsync<string>(sql);

            // Sinh mã nhân viên mới
            var newEmployeeCode = GenerateNewCode(result);

            return newEmployeeCode;
        }

        /// <summary>
        /// Sinh mã nhân viên mới từ mã nhân viên lớn nhất trong database
        /// </summary>
        /// <param name="maxEmployeeCode">Mã nhân viên lớn nhất trong database</param>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        public string GenerateNewCode(string maxEmployeeCode)
        {
            // Sinh mã nhân viên mới
            var newEmployeeCode = string.Empty;

            if (maxEmployeeCode == null)
            {
                newEmployeeCode = "NV-0000001";
            }
            else
            {
                var oldCodeNumber = maxEmployeeCode.Substring(3);
                var newCodeNumber = (int.Parse(oldCodeNumber) + 1).ToString().PadLeft(7, '0');
                newEmployeeCode = $"NV-{newCodeNumber}";
            }

            return newEmployeeCode;
        }
    }
}
