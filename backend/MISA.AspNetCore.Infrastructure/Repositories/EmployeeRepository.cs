﻿using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using static Dapper.SqlMapper;
using System.Resources;
using System.Text.RegularExpressions;

namespace MISA.AspNetCore.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbConnectionService dbConnectionService) : base(dbConnectionService)
        {
        }

        /// <summary>
        /// Xuất file excel danh sách nhân viên theo bộ lọc
        /// </summary>
        /// <param name="search">Từ khoá tìm kiếm (Mã nhân viên, Họ và tên, Số điện thoại)</param>
        /// <param name="orders">Sắp xếp theo các trường (VD: EmployeeCode, +FullName, -PhoneNumber)</param>
        /// <returns>File danh sách nhân viên theo kết quả lọc</returns>
        /// CreatedBy: hiennt200210 (02/10/2023)
        //public async Task<Export> ExportAsync(string? search, List<string>? orders)
        //{
        //    // Lấy danh sách nhân viên theo bộ lọc
        //    var employees = await PagingAsync(0, 0, search, search, search, orders);
        //}

        /// <summary>
        /// Lấy thông tin nhân viên theo bộ lọc, tìm kiếm, sắp xếp, phân trang
        /// </summary>
        /// <param name="limit">Số lượng bản ghi trên một trang</param>
        /// <param name="offset">Vị trí bắt đầu lấy dữ liệu</param>
        /// <returns>Danh sách nhân viên theo kết quá lọc, tìm kiếm, sắp xếp, phân trang</returns>
        public async Task<List<Employee>> PagingAsync(int limit, int offset, string employeeCode, string fullName, string phoneNumber, List<string> orders)
        {
            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@limit", limit);
            parameters.Add("@offset", offset);

            // Xử lý tham số tìm kiếm
            var conditions = "WHERE ";

            if (employeeCode != null)
            {
                conditions += $"{base.TableName}Code LIKE @employeeCode OR ";
                parameters.Add("@employeeCode", $"%{employeeCode}%");
            }

            if (fullName != null)
            {
                conditions += $"FullName LIKE @fullName OR ";
                parameters.Add("@fullName", $"%{fullName}%");
            }

            if (phoneNumber != null)
            {
                conditions += $"PhoneNumber LIKE @phoneNumber OR ";
                parameters.Add("@phoneNumber", $"%{phoneNumber}%");
            }

            // Xóa OR ở cuối chuỗi
            if (conditions.Length > 6)
            {
                conditions = conditions.Remove(conditions.Length - 4);
            }

            if (conditions == "WHERE ")
            {
                conditions = "";
            }

            // Xử lý tham số sắp xếp
            var ordersString = string.Empty;

            if (orders.Count == 0)
            {
                ordersString += $"{base.TableName}Code ASC";
            }
            else
            {
                // Xử lý chuỗi order
                foreach (var order in orders)
                {
                    if (order.Contains('-'))
                    {
                        ordersString += $"{order.Replace("-", "")} DESC, ";
                    }
                    else
                    {
                        ordersString += $"{order.Replace("+", "")} ASC, ";
                    }
                }

                ordersString = ordersString.Remove(ordersString.Length - 2);
            }

            // Tạo kết nối với database
            var connection = base.DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {base.TableName} {conditions} ORDER BY {ordersString} LIMIT @limit OFFSET @offset;";

            if (limit == 0)
            {
                sql = $"SELECT * FROM {base.TableName} {conditions} ORDER BY {ordersString};";
            }

            // Thực thi câu truy vấn
            var result = await connection.QueryAsync<Employee>(sql, parameters);

            // Nếu không có kết quả thì báo lỗi
            return result == null
                ? throw new NotFoundException()
                {
                    ErrorCode = ErrorCode.NotFound,
                    DevMessage = Domain.Resources.Errors.NotFound,
                    UserMessage = Domain.Resources.Errors.NotFound,
                    Errors = Domain.Resources.Errors.NotFound,
                    TraceId = "",
                    MoreInfo = "",
                }
                : result.ToList();
        }

        /// <summary>
        /// Lấy số lượng bản ghi
        /// </summary>
        /// <returns>Số lượng bản ghi</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> CountAsync()
        {
            // Tạo kết nối với database
            var connection = base.DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT COUNT(*) FROM {base.TableName};";

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleOrDefaultAsync<int>(sql);

            return result;
        }

        /// <summary>
        /// Lấy số lượng bản ghi của kết quả tìm kiếm
        /// </summary>
        /// <returns>Số lượng bản ghi của kết quả tìm kiếm</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> CountAsync(string employeeCode, string fullName, string phoneNumber)
        {
            // Tạo dynamic parameter
            var parameters = new DynamicParameters();

            // Xử lý tham số tìm kiếm
            var conditions = "WHERE ";

            if (employeeCode != null)
            {
                conditions += $"{base.TableName}Code LIKE @employeeCode OR ";
                parameters.Add("@employeeCode", $"%{employeeCode}%");
            }

            if (fullName != null)
            {
                conditions += $"FullName LIKE @fullName OR ";
                parameters.Add("@fullName", $"%{fullName}%");
            }

            if (phoneNumber != null)
            {
                conditions += $"PhoneNumber LIKE @phoneNumber OR ";
                parameters.Add("@phoneNumber", $"%{phoneNumber}%");
            }

            // Xóa AND ở cuối chuỗi
            if (conditions.Length > 0)
            {
                conditions = conditions.Remove(conditions.Length - 4);
            }

            // Tạo kết nối với database
            var connection = base.DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT COUNT(*) FROM {base.TableName} {conditions};";

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleOrDefaultAsync<int>(sql, parameters);

            return result;
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
                    ErrorCode = ErrorCode.Conflict,
                    DevMessage = Domain.Resources.Errors.Conflict,
                    UserMessage = Domain.Resources.Errors.Conflict,
                    Errors = new List<string>()
                    {
                        Domain.Resources.Errors.EmployeeCodeDuplicated,
                    },
                    TraceId = "",
                    MoreInfo = "",
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
        public static string GenerateNewCode(string maxEmployeeCode)
        {
            string? newEmployeeCode;

            if (maxEmployeeCode == null)
            {
                newEmployeeCode = "NV-0001";
            }
            else
            {
                Match match = Regex.Match(maxEmployeeCode, @"\d+");

                if (match.Success)
                {
                    string numberPart = match.Value;
                    int number = int.Parse(numberPart);

                    number++;

                    string result = maxEmployeeCode.Replace(numberPart, number.ToString("D" + numberPart.Length));

                    return result;
                }
                else
                {
                    return maxEmployeeCode + "1";
                }
            }

            return newEmployeeCode;
        }
    }
}
