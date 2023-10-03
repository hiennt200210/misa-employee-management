using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy thông tin nhân viên theo bộ lọc, tìm kiếm, sắp xếp, phân trang
        /// </summary>
        /// <param name="limit">Số lượng bản ghi trên một trang</param>
        /// <param name="offset">Vị trí bắt đầu lấy dữ liệu</param>
        /// <returns>Danh sách nhân viên theo kết quá lọc, tìm kiếm, sắp xếp, phân trang</returns>
        Task<List<Employee>> PagingAsync(int limit, int offset, string employeeCode, string fullName, string phoneNumber, List<string> orders);

        /// <summary>
        /// Lấy số lượng bản ghi
        /// </summary>
        /// <returns>Số lượng bản ghi</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> CountAsync();

        /// <summary>
        /// Lấy số lượng bản ghi của kết quả tìm kiếm
        /// </summary>
        /// <returns>Số lượng bản ghi của kết quả tìm kiếm</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> CountAsync(string employeeCode, string fullName, string phoneNumber);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns>False nếu mã nhân viên chưa tồn tại</returns>
        /// <exception cref="ConflictException">Mã nhân viên đã tồn tại</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task CheckDuplicateEmployeeCodeAsync(string employeeCode);
    }
}

