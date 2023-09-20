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

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await BaseRepository.GetByIdAsync(id);
            var dto = MapEntityToDto(entity);
            return dto;
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();
            var dtos = entities.Select(entity => MapEntityToDto(entity)).ToList();
            return dtos;
        }

        public abstract TDto MapEntityToDto(TEntity entity);
    }
}
