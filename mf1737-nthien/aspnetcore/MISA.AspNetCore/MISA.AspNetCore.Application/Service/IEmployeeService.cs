using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<List<EmployeeModel>> GetAllAsync();

        /// <summary>
        /// Lấy thông tin một nhân viên
        /// </summary>
        /// <param name="employeeId">Định danh của nhân viên cần lấy thông tin</param>
        /// <returns>Thông tin nhân viên</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<EmployeeModel> GetByIdAsync(Guid employeeId);

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="employeeModel">Thông tin nhân viên cần thêm mới</param>
        /// <returns>Thông tin nhân viên cần thêm mới</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(EmployeeInsertModel employeeInsertModel);

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thông tin cần cập nhật</param>
        /// <returns>Thông tin cần cập nhật</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(EmployeeUpdateModel employeeUpdateModel);

        /// <summary>
        /// Xóa một nhân viên
        /// </summary>
        /// <param name="employeeId">Định danh của nhân viên cần xóa</param>
        /// <returns>
        /// 1 - Xóa thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid employeeId);

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeeId">Danh sách định danh của các nhân viên cần xóa</param>
        /// <returns>
        /// Số lượng nhân viên đã xóa thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteMultipleAsync(List<Guid> employeeId);
    }
}
