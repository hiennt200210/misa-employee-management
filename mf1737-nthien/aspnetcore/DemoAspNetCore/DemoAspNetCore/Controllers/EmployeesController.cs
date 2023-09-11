using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace DemoAspNetCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Thông tin kết nối với database
        /// </summary>
        public readonly string ConnectionString = "Server=localhost; Port=3306; Database=MISA.DemoDB; Uid=root; Password=1234;";

        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên.
        /// </summary>
        /// 
        /// <returns>
        /// 200 - Danh sách nhân viên
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            try
            {
                // Khai báo thông tin kết nối với database

                // Khởi tạo đối tượng kết nối với database
                var connection = new MySqlConnection(ConnectionString);

                // Tạo câu truy vấn
                var sql = "CALL Proc_GetAllEmployees();";

                // Thực thi câu truy vấn
                var result = await connection.QueryAsync<EmployeeEntity>(sql);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo id.
        /// </summary>
        /// 
        /// <param name="employeeId">Định danh của nhân viên</param>
        /// 
        /// <returns>
        /// 200 - Thông tin nhân viên
        /// 500 - Lỗi phía server
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid employeeId)
        {
            try
            {
                // Khai báo thông tin kết nối với database

                // Khởi tạo đối tượng kết nối với database
                var connection = new MySqlConnection(ConnectionString);

                // Tạo câu truy vấn
                var sql = "CALL Proc_GetEmployeeById(@employeeId);";

                // Tạo dynamicParam
                var param = new DynamicParameters();
                param.Add("@employeeId", employeeId);

                // Thực thi câu truy vấn
                var result = await connection.QuerySingleAsync<EmployeeEntity>(sql, param);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới nhân viên.
        /// </summary>
        /// 
        /// <param name="employee">Thông tin nhân viên</param>
        /// 
        /// <returns>
        /// 201 - Thêm mới thành công
        /// 400 - Dữ liệu không hợp lệ
        /// 500 - Lỗi không xác định
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeEntity employee)
        {
            try
            {
                var ErrorData = new Dictionary<string, string>();

                // Validate dữ liệu bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    ErrorData.Add("employeeCode", Resources.VN.Errot_InputValidation_EmployeeCodeCannotBeEmpty);
                }

                if (string.IsNullOrEmpty(employee.FullName))
                {
                    ErrorData.Add("fullName", Resources.VN.Errot_InputValidation_FullNameCannotBeEmpty);
                }

                // Validate định dạng email
                if (employee.Email != null && IsValidEmail(employee.Email) == false)
                {
                    ErrorData.Add("email", Resources.VN.Errot_InputValidation_EmailInvalid);
                }

                // Kiểm tra mã nhân viên đã tồn tại hay chưa
                if (employee.EmployeeCode != null && EmployeeCodeIsExists(employee.EmployeeCode) == true)
                {
                    ErrorData.Add("employeeCodeIsExists", Resources.VN.Errot_InputValidation_EmployeeCodeIsExists);
                }

                if (ErrorData.Count > 0)
                {
                    var error = new ErrorService
                    {
                        DevMsg = Resources.VN.Error_InputValidation,
                        UserMsg = Resources.VN.Error_InputValidation,
                        MoreInfo = ErrorData,
                    };

                    return BadRequest(error);
                }

                // Khởi tạo đối tượng kết nối với database
                var connection = new MySqlConnection(ConnectionString);

                // Tạo câu truy vấn INSERT
                var sql = "CALL Proc_CreateEmployee(@EmployeeId, @EmployeeCode, @FullName, @Gender, @DateOfBirth, @PositionName, @DepartmentId, @PhoneNumber, @Landline, @Email, @Address, @IdentityNumber, @IdentityDate, @IdentityPlace, @BankAccount, @BankName, @BankBranch, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy);";

                // Tạo mới EmployeeId
                employee.EmployeeId = Guid.NewGuid();
                employee.CreatedDate = DateTime.Now;
                employee.ModifiedDate = DateTime.Now;

                // Thực thi câu truy vấn
                var affectedRows = await connection.ExecuteAsync(sql, employee);

                // Kiểm tra xem có nhân viên được thêm mới thành công hay không
                if (affectedRows > 0)
                {
                    return StatusCode(201, affectedRows);
                }
                else
                {
                    return Ok(affectedRows);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa thông tin nhân viên theo id.
        /// </summary>
        /// 
        /// <param name="employeeId">Định danh của nhân viên cần sửa</param>
        /// <param name="employee">Thông tin nhân viên sau khi sửa</param>
        /// 
        /// <returns>
        /// 200 - Sửa thành công
        /// 500 - Lỗi phía server
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        [HttpPut]
        [Route("{employeeId}")]
        public async Task<IActionResult> ModifyEmployeeAsync(Guid employeeId, [FromBody] EmployeeEntity employee)
        {
            try
            {
                var ErrorData = new Dictionary<string, string>();

                // Validate dữ liệu bắt buộc nhập
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    ErrorData.Add("employeeCode", Resources.VN.Errot_InputValidation_EmployeeCodeCannotBeEmpty);
                }

                if (string.IsNullOrEmpty(employee.FullName))
                {
                    ErrorData.Add("fullName", Resources.VN.Errot_InputValidation_FullNameCannotBeEmpty);
                }

                // Validate định dạng email
                if (employee.Email != null && IsValidEmail(employee.Email) == false)
                {
                    ErrorData.Add("email", Resources.VN.Errot_InputValidation_EmailInvalid);
                }

                // Kiểm tra mã nhân viên đã tồn tại hay chưa
                if (employee.EmployeeCode != null && EmployeeCodeIsExists(employee.EmployeeCode) == true)
                {
                    ErrorData.Add("employeeCodeIsExists", Resources.VN.Errot_InputValidation_EmployeeCodeIsExists);
                }

                if (employeeId != employee.EmployeeId)
                {
                    ErrorData.Add("employeeIdInvalid", Resources.VN.Error_InputValidation_IdInvalid);
                }

                if (ErrorData.Count > 0)
                {
                    var error = new ErrorService
                    {
                        DevMsg = Resources.VN.Error_InputValidation,
                        UserMsg = Resources.VN.Error_InputValidation,
                        MoreInfo = ErrorData,
                    };

                    return BadRequest(error);
                }

                var connection = new MySqlConnection(ConnectionString);

                var sql = "CALL Proc_UpdateEmployee(@EmployeeId, @EmployeeCode, @FullName, @Gender, @DateOfBirth, @PositionName, @DepartmentId, @PhoneNumber, @Landline, @Email, @Address, @IdentityNumber, @IdentityDate, @IdentityPlace, @BankAccount, @BankName, @BankBranch, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy);";

                employee.ModifiedDate = DateTime.Now;

                var affectedRows = await connection.ExecuteAsync(sql, employee);

                if (affectedRows > 0)
                {
                    return Ok(affectedRows);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xoá nhân viên theo id.
        /// </summary>
        /// 
        /// <param name="employeeId">Định danh của nhân viên</param>
        /// 
        /// <returns>
        /// 200 - Xoá thành công
        /// 500 - Lỗi phía server
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid employeeId)
        {
            try
            {
                var connection = new MySqlConnection(ConnectionString);

                var sql = "CALL Proc_DeleteEmployee(@employeeId);";

                var param = new DynamicParameters();
                param.Add("@employeeId", employeeId);

                var affectedRows = await connection.ExecuteAsync(sql, param);
                if (affectedRows > 0)
                {
                    return Ok(affectedRows);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Validate email.
        /// </summary>
        /// 
        /// <param name="email">Chuỗi địa chỉ email</param>
        /// 
        /// <returns>
        /// True - Email hợp lệ
        /// False - Email không hợp lệ
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        private static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa.
        /// </summary>
        /// 
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// 
        /// <returns>
        /// true - Mã nhân viên đã tồn tại
        /// false - Mã nhân viên chưa tồn tại
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        private bool EmployeeCodeIsExists(string employeeCode)
        {
            var connection = new MySqlConnection(ConnectionString);

            var sql = "CALL Proc_GetAllEmployeeCode(@employeeCode);";

            var param = new DynamicParameters();
            param.Add("@employeeCode", employeeCode);
            
            var result = connection.QueryFirstOrDefault(sql, param);
            
            return result ? true : false;
        }

        /// <summary>
        /// Xử lý ngoại lệ.
        /// </summary>
        /// 
        /// <param name="exception">Ngoại lệ</param>
        /// 
        /// <returns>
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (11/09/2023)
        private IActionResult HandleException(Exception exception)
        {
            int statusCode;
            var error = new ErrorService
            {
                ErrorCode = "",
                DevMsg = "",
                UserMsg = "",
                MoreInfo = exception.Message,
                TraceId = HttpContext.TraceIdentifier,
            };

            if (exception is ValidationException)
            {
                statusCode = 400;
                error.DevMsg = Resources.VN.Error_InputValidation;
                error.UserMsg = Resources.VN.Error_InputValidation;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = 401;
                error.DevMsg = Resources.VN.Error_Authentication;
                error.UserMsg = Resources.VN.Error_Authentication;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = 403;
                error.DevMsg = Resources.VN.Error_AccessDenied;
                error.UserMsg = Resources.VN.Error_AccessDenied;
            }
            else if (exception is InvalidOperationException)
            {
                statusCode = 404;
                error.DevMsg = Resources.VN.Error_NotFound;
                error.UserMsg = Resources.VN.Error_NotFound;
            }
            else
            {
                statusCode = 500;
                error.DevMsg = Resources.VN.Error_Service_Dev;
                error.UserMsg = Resources.VN.Error_Service_User;
            }

            return StatusCode(statusCode, error);
        }
    }
}
