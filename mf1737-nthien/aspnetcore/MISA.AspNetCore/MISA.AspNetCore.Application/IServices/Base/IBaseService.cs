using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IBaseService<TDto, TInsertDto, TUpdateDto> : IBaseReadOnlyService<TDto>
    {
        /// <summary>
        /// Thêm mới một đối tượng
        /// </summary>
        /// <param name="insertDto">Đối tượng cần thêm mới</param>
        /// <returns>1 (Thêm mới thành công)</returns>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(TInsertDto insertDto);

        /// <summary>
        /// Cập nhật một đối tượng
        /// </summary>
        /// <param name="id">Định danh của đối tượng cần cập nhật</param>
        /// <param name="updateDto">Đối tượng mới cần cập nhật</param>
        /// <returns>1 (Cập nhật thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng cần cập nhật</exception>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(Guid id, TUpdateDto updateDto);

        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="id">Định danh của đối tượng cần xóa</param>
        /// <returns>1 (Xóa thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng cần xóa</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid id);
    }
}
