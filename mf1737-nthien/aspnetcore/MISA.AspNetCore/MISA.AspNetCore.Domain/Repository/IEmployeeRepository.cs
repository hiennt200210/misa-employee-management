using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Tìm kiếm nhân viên theo định danh
        /// </summary>
        /// <param name="employeeId">Định danh của nhân viên cần tìm</param>
        /// <returns>
        /// Thông tin nhân viên cần tìm
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<Employee?> FindAsync(Guid employeeId);

        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Thông tin nhân viên</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<List<Employee>> GetAllAsync();

        /// <summary>
        /// Lấy thông tin một nhân viên
        /// </summary>
        /// <param name="employeeId">Định danh của nhân viên cần lấy thông tin</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>Thông tin nhân viên</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<Employee> GetByIdAsync(Guid employeeId);

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm mới</param>
        /// <returns>
        /// 1 - Thêm mới thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(Employee employee);

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee">Thông tin cần cập nhật</param>
        /// <returns>
        /// 1 - Cập nhật thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(Employee employee);

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
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns></returns>
        Task<Employee> CheckEmployeeCodeExistsAsync(string employeeCode);
    }
}
