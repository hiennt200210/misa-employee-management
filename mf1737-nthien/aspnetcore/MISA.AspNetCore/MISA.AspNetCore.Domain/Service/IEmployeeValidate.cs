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
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <exception cref="ConflictException">Mã nhân viên đã tồn tại</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task CheckEmployeeCodeExists(string employeeCode);
    }
}
