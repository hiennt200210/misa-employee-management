using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        Task<string> GetNewEmployeeCodeAsync();
    }
}
