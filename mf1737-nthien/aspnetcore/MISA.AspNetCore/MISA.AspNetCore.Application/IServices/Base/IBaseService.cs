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
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="dto">Thông tin cần thêm mới</param>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(TInsertDto insertDto);

        /// <summary>
        /// Thêm mới nhiều bản ghi
        /// </summary>
        /// <param name="dtos">Thông tin cần thêm mới</param>
        /// <returns></returns>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<int> InsertManyAsync(List<TInsertDto> insertDtos);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="dto">Thông tin cần cập nhật</param>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(Guid id, TUpdateDto updateDto);

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="">Thông tin cần cập nhật</param>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<int> UpdateManyAsync(List<Guid> ids, List<TUpdateDto> updateDtos);

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="dto">Thông tin cần xóa</param>
        /// <returns></returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="dtos">Thông tin cần xóa</param>
        /// <returns></returns>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
