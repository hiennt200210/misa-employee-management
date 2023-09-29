using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;

namespace MISA.AspNetCore.Web.Controllers
{
    public class EmployeesController : BaseController<EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>
    {
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo bộ lọc
        /// </summary>
        /// <param name="specs">Bộ lọc</param>
        /// <returns>Thông tin nhân viên theo bộ lọc</returns>
        /// <exception cref="NotFoundException">Không tìm thấy nhân viên nào theo bộ lọc</exception>
        /// CreatedBy: hiennt200210 (22/09/2023)
        [HttpGet]
        [Route("Pagination")]
        public async Task<PaginationDto> PagingAsync([FromQuery] int limit, [FromQuery] int offset, [FromQuery] string? employeeCode, [FromQuery] string? fullName, [FromQuery] string? phoneNumber, [FromQuery] List<string>? orders)
        {
            var result = await (BaseService as IEmployeeService).PagingAsync(limit, offset, employeeCode, fullName, phoneNumber, orders);
            return result;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        [HttpGet]
        [Route("NewEmployeeCode")]
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var result = await (BaseService as IEmployeeService).GetNewEmployeeCodeAsync();
            return result;
        }
    }
}
