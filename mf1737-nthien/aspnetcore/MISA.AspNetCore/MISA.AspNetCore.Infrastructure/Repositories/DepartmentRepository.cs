using Dapper;
using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Infrastructure
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IDbConnectionService dbConnectionService) : base(dbConnectionService)
        {
        }

        /// <summary>
        /// Kiểm tra trùng tên phòng ban
        /// </summary>
        /// <param name="departmentName">Tên phòng ban cần kiểm tra</param>
        /// <exception cref="ConflictException">Tên phòng ban đã tồn tại</exception>
        /// <returns>False nếu tên phòng ban chưa tồn tại</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task CheckDuplicateDepartmentNameAsync(string departmentName)
        {
            // Tạo kết nối với database
            var connection = base.DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {base.TableName} WHERE {base.TableName}Name = @departmentName";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@departmentName", departmentName);

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleAsync<Employee>(sql, parameters);

            if (result != null)
            {
                throw new ConflictException()
                {
                    DevMessage = Domain.Resources.Errors.Conflict,
                    UserMessage = Domain.Resources.Errors.Conflict,
                    Errors = Domain.Resources.Errors.DepartmentNameDuplicated
                };
            }
        }
    }
}
