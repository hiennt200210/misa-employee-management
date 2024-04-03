using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Kiểm tra trùng tên phòng ban
        /// </summary>
        /// <param name="employeeCode">Tên phòng ban cần kiểm tra</param>
        /// <returns>False nếu tên phòng ban chưa tồn tại</returns>
        /// <exception cref="ConflictException">Tên phòng ban đã tồn tại</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task CheckDuplicateDepartmentNameAsync(string departmentName);
    }
}

