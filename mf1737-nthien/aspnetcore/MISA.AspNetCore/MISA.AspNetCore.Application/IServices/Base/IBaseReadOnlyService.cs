using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IBaseReadOnlyService<TDto>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi nào</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<List<TDto>> GetAllAsync();

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần lấy</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<TDto> GetByIdAsync(Guid id);
    }
}
