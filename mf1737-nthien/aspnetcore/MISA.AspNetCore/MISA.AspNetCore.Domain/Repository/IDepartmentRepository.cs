using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Tìm kiếm phòng ban theo định danh
        /// </summary>
        /// <param name="departmentId">Định danh của phòng ban cần tìm</param>
        /// <returns>
        /// Thông tin phòng ban cần tìm
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<Department?> FindAsync(Guid departmentId);

        /// <summary>
        /// Lấy thông tin một phòng ban
        /// </summary>
        /// <param name="departmentId">Định danh của phòng ban cần lấy thông tin</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Thông tin một phòng ban</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<Department> GetAsync(Guid departmentId);

        /// <summary>
        /// Thêm mới một phòng ban
        /// </summary>
        /// <param name="department">Thông tin phòng ban cần thêm mới</param>
        /// <returns>
        /// 1 - Thêm mới thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(Department department);

        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="department">Thông tin cần cập nhật</param>
        /// <returns>
        /// 1 - Cập nhật thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(Department department);

        /// <summary>
        /// Xóa một phòng ban
        /// </summary>
        /// <param name="departmentId">Định danh của phòng ban cần xóa</param>
        /// <returns>
        /// 1 - Xóa thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid departmentId);
    }
}
