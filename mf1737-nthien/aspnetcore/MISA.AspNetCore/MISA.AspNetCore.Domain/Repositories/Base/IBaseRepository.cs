using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IBaseRepository<TEntity>
    {
        #region Methods

        /// <summary>
        /// Lấy một bản ghi
        /// </summary>
        /// <param name="entityId">Định danh của bản ghi cần lấy</param>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<TEntity> GetByIdAsync(Guid entityId);

        Task<List<TEntity>> GetByListIdAsync(List<Guid> ids);

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin cần thêm mới</param>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Thêm mới nhiều bản ghi
        /// </summary>
        /// <param name="entities">Thông tin cần thêm mới</param>
        /// <returns></returns>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<int> InsertManyAsync(List<TEntity> entities);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin cần cập nhật</param>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="">Thông tin cần cập nhật</param>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<int> UpdateManyAsync(List<Guid> ids, List<TEntity> entities);

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin cần xóa</param>
        /// <returns></returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entities">Thông tin cần xóa</param>
        /// <returns></returns>
        /// CreatedBy: hiennt200210 (20/09/2023)
        Task<int> DeleteManyAsync(List<Guid> ids);

        #endregion
    }
}
