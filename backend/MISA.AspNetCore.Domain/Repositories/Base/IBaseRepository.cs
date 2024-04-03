using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi nào</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần lấy</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm mới</param>
        /// <returns>1 (Thêm mới thành công)</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần cập nhật</param>
        /// <param name="entity">Bản ghi mới cần cập nhật</param>
        /// <returns>1 (Cập nhật thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần cập nhật</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần xóa</param>
        /// <returns>1 (Xóa thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần xóa</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách định danh của các bản ghi cần xóa</param>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần xóa</exception>"
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// CreatedBy: hiennt200210 (25/09/2023)
        Task<int> DeleteMultipleAsync(List<Guid> ids);
    }
}
