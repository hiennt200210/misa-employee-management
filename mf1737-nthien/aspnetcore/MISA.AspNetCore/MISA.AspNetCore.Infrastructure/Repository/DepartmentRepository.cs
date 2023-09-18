using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AspNetCore.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Infrastructure
{
    public class DepartmentRepository
    {
        private readonly IConfiguration _configuration;

        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_GetAllDepartment();";

            // Thực thi câu truy vấn
            var departments = await connection.QueryAsync<Department>(sql);

            return departments.ToList();
        }

        public async Task<Department> GetByIdAsync(Guid departmentId)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_GetDepartmentById(@DepartmentId);";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentId);

            // Thực thi câu truy vấn
            var department = await connection.QuerySingleAsync<Department>(sql, parameters);

            return department;
        }

        public async Task<int> InsertAsync(Department department)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_InsertEmployee(@CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy, @DepartmentId, @DepartmentName);";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@CreatedDate", department.CreatedDate);
            parameters.Add("@CreatedBy", department.CreatedBy);
            parameters.Add("@ModifiedDate", department.ModifiedDate);
            parameters.Add("@ModifiedBy", department.ModifiedBy);
            parameters.Add("@DepartmentId", department.DepartmentId);
            parameters.Add("@DepartmentName", department.DepartmentName);


            // Thực thi câu truy vấn
            var affectedRows = await connection.ExecuteAsync(sql, parameters);

            return affectedRows;
        }

        public async Task<int> UpdateAsync(Department department)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_UpdateEmployee(@CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy, @DepartmentId, @DepartmentName);";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@CreatedDate", department.CreatedDate);
            parameters.Add("@CreatedBy", department.CreatedBy);
            parameters.Add("@ModifiedDate", department.ModifiedDate);
            parameters.Add("@ModifiedBy", department.ModifiedBy);
            parameters.Add("@DepartmentId", department.DepartmentId);
            parameters.Add("@DepartmentName", department.DepartmentName);

            var affectedRows = await connection.ExecuteAsync(sql, parameters);

            return affectedRows;
        }

        public async Task<int> DeleteAsync(Guid departmentId)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_DeleteDepartment(@DepartmentId);";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentId", departmentId);

            // Thực thi câu truy vấn
            var result = await connection.ExecuteAsync(sql, parameters);

            return result;
        }
    }
}
