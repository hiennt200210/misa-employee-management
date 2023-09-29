using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public abstract class BaseReadOnlyService<TEntity, TDto> : IBaseReadOnlyService<TDto>
    {
        protected readonly IBaseRepository<TEntity> BaseRepository;

        public BaseReadOnlyService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi nào</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();

            List<TDto> dtos = new List<TDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                TEntity entity = entities[i];
                var dto = await MapEntityToDto(entity);
                dtos.Add(dto);
            }

            return dtos;
        }

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần lấy</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await BaseRepository.GetByIdAsync(id);
            var dto = await MapEntityToDto(entity);
            return dto;
        }

        /// <summary>
        /// Chuyển đổi từ Employee sang EmployeeDto
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public abstract Task<TDto> MapEntityToDto(TEntity entity);
    }
}
