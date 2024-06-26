﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>
    {
        /// <summary>
        /// Lấy thông tin nhân viên theo bộ lọc, tìm kiếm, sắp xếp, phân trang
        /// </summary>
        /// <param name="limit">Số lượng bản ghi trên một trang</param>
        /// <param name="offset">Vị trí bắt đầu lấy dữ liệu</param>
        /// <returns>Danh sách nhân viên theo kết quá lọc, tìm kiếm, sắp xếp, phân trang</returns>
        Task<PaginationDto> PagingAsync(int limit, int offset, string employeeCode, string fullName, string phoneNumber, List<string> orders);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Xuất file danh sách nhân viên theo bộ lọc
        /// </summary>
        /// <param name="search">Từ khoá tìm kiếm (Mã nhân viên, Họ và tên, Số điện thoại)</param>
        /// <param name="orders">Sắp xếp theo các trường (VD: EmployeeCode, +FullName, -PhoneNumber)</param>
        /// <returns>File danh sách nhân viên theo kết quả lọc</returns>
        /// CreatedBy: hiennt200210 (02/10/2023)
        Task<EmployeeExportDto> ExportAsync(string? search, List<string>? orders);
    }
}
