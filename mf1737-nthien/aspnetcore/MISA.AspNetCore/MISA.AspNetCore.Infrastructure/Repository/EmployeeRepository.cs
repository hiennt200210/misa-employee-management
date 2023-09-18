using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySqlConnector;

namespace MISA.AspNetCore.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Employee?> FindAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetAsync(Guid employeeId)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_GetEmployeeById(@EmployeeId);";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleAsync<Employee>(sql, parameters);

            return result;
        }

        public async Task<int> InsertAsync(Employee employee)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "CALL Proc_InsertEmployee(@EmployeeId);";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@CreatedDate", employee.CreatedDate);
            parameters.Add("@CreatedBy", employee.CreatedBy);
            parameters.Add("@ModifiedDate", employee.ModifiedDate);
            parameters.Add("@ModifiedBy", employee.ModifiedBy);
            parameters.Add("@EmployeeId", employee.EmployeeId);
            parameters.Add("@EmployeeCode", employee.EmployeeCode);
            parameters.Add("@FullName", employee.FullName);
            parameters.Add("@Gender", employee.Gender);
            parameters.Add("@DateOfBirth", employee.DateOfBirth);
            parameters.Add("@PositionName", employee.PositionName);
            parameters.Add("@DepartmentId", employee.DepartmentId);
            parameters.Add("@PhoneNumber", employee.PhoneNumber);
            parameters.Add("@LandlineNumber", employee.LandlineNumber);
            parameters.Add("@Email", employee.Email);
            parameters.Add("@Address", employee.Address);
            parameters.Add("@IdentityNumber", employee.IdentityNumber);
            parameters.Add("@IdentityDate", employee.IdentityDate);
            parameters.Add("@IdentityPlace", employee.IdentityPlace);
            parameters.Add("@BankAccount", employee.BankAccount);
            parameters.Add("@BankName", employee.BankName);
            parameters.Add("@BankBranch", employee.BankBranch);

            // Thực thi câu truy vấn
            var affectedRows = await connection.ExecuteAsync(sql, parameters);

            return affectedRows;
        }

        public Task<int> UpdateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> CheckEmployeeCodeExistsAsync(string employeeCode)
        {
            // Lấy ra connection string
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Khởi tạo đối tượng kết nối với database
            var connection = new MySqlConnection(connectionString);

            // Tạo câu truy vấn
            var sql = "SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode;";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);

            // Thực thi câu truy vấn
            var result = await connection.QueryFirstOrDefaultAsync<Employee>(sql, parameters);

            return result;
        }
    }
}
