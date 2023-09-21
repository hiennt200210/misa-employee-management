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
        /// Lấy một bản ghi
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần lấy</param>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<TDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<List<TDto>> GetAllAsync();
    }
}
