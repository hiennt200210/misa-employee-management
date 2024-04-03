using AutoMapper;
using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Domain.Entities.Base;
using System.Security.Principal;

namespace MISA.AspNetCore.Application
{
    public abstract class BaseService<TEntity, TDto, TInsertDto, TUpdateDto> : BaseReadOnlyService<TEntity, TDto>, IBaseService<TDto, TInsertDto, TUpdateDto> where TEntity : Domain.TEntity
    {
        public BaseService(IBaseRepository<TEntity> baseRepository) : base(baseRepository)
        {
        }

        /// <summary>
        /// Thêm mới một đối tượng
        /// </summary>
        /// <param name="insertDto">Đối tượng cần thêm mới</param>
        /// <returns>1 (Thêm mới thành công)</returns>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> InsertAsync(TInsertDto insertDto)
        {
            var entity = MapInsertDtoToEntity(insertDto);
            await ValidateBusinessInsertAsync(entity);
            var result = await BaseRepository.InsertAsync(entity);
            return result;
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// </summary>
        /// <param name="id">Định danh của đối tượng cần cập nhật</param>
        /// <param name="updateDto">Đối tượng mới cần cập nhật</param>
        /// <returns>1 (Cập nhật thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng cần cập nhật</exception>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var oldEntity = await BaseRepository.GetByIdAsync(id);
            var newEntity = MapUpdateDtoToEntity(updateDto, oldEntity);
            await ValidateBusinessUpdateAsync(oldEntity, newEntity);
            var result = await BaseRepository.UpdateAsync(id, newEntity);
            return result;
        }

        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="id">Định danh của đối tượng cần xóa</param>
        /// <returns>1 (Xóa thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng cần xóa</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await BaseRepository.DeleteAsync(id);
            return result;
        }

        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="ids">Danh sách định danh của các đối tượng cần xóa</param>
        /// <returns>Số lượng đối tượng đã xóa</returns>
        /// CreatedBy: hiennt200210 (25/09/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var result = await BaseRepository.DeleteMultipleAsync(ids);
            return result;
        }

        /// <summary>
        /// Chuyển đổi từ EmployeeInsertDto sang Employee
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public abstract TEntity MapInsertDtoToEntity(TInsertDto insertDto);

        /// <summary>
        /// Chuyển đổi từ EmployeeUpdateDto sang Employee
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity oldEntity);

        /// <summary>
        /// Validate nghiệp vụ khi thêm mới
        /// </summary>
        /// <param name="entity">Dữ liệu cần validate</param>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public abstract Task ValidateBusinessInsertAsync(TEntity entity);

        /// <summary>
        /// Validate nghiệp vụ khi cập nhật
        /// </summary>
        /// <param name="oldEntity">Dữ liệu cũ</param>
        /// <param name="newEntity">Dữ liệu mới</param>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public abstract Task ValidateBusinessUpdateAsync(TEntity oldEntity, TEntity newEntity);
    }
}
