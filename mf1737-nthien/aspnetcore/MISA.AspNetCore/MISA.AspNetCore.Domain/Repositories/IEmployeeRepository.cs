using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        #region Methods

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <exception cref="ConflictException">
        /// Nếu mã nhân viên đã tồn tại
        /// </exception>"
        /// <returns>
        /// False nếu mã nhân viên chưa tồn tại
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<bool> CheckDuplicateEmployeeCodeAsync(string employeeCode);

        #endregion
    }
}

