using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IEmployeeValidate
    {
        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên cần kiểm tra</param>
        /// <exception cref="ConflictException">Mã nhân viên đã tồn tại</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<bool> CheckDuplicateEmployeeCode(Employee employee);
    }
}
