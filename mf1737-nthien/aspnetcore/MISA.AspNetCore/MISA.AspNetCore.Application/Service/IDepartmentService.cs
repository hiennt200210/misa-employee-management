using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<List<DepartmentModel>> GetAllAsync();

        /// <summary>
        /// Lấy thông tin một phòng ban
        /// </summary>
        /// <param name="employeeId">Định danh của phòng ban cần lấy thông tin</param>
        /// <returns>Thông tin phòng ban</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<DepartmentModel> GetByIdAsync(Guid employeeId);

        /// <summary>
        /// Thêm mới một phòng ban
        /// </summary>
        /// <param name="employeeModel">Thông tin phòng ban cần thêm mới</param>
        /// <returns>Thông tin phòng ban cần thêm mới</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(DepartmentInsertModel employeeInsertModel);

        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="employee">Thông tin cần cập nhật</param>
        /// <returns>Thông tin cần cập nhật</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(DepartmentUpdateModel employeeUpdateModel);

        /// <summary>
        /// Xóa một phòng ban
        /// </summary>
        /// <param name="employeeId">Định danh của phòng ban cần xóa</param>
        /// <returns>
        /// 1 - Xóa thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid employeeId);
    }
}
