using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public abstract class BaseService<TEntity, TDto, TInsertDto, TUpdateDto> : BaseReadOnlyService<TEntity, TDto>, IBaseService<TDto, TInsertDto, TUpdateDto> where TEntity : IEntity
    {
        protected BaseService(IBaseRepository<TEntity> baseRepository) : base(baseRepository)
        {
        }

        public Task<TDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TDto> InsertAsync(TInsertDto insertDto)
        {
            var entity = MapInsertDtoToEntity(insertDto);

            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedDate = DateTime.Now;
                baseEntity.CreatedBy ??= "hiennt200210";
                baseEntity.ModifiedDate = DateTime.Now;
                baseEntity.ModifiedBy ??= "hiennt200210";
            }    
            
            await ValidateInsertBusiness(entity);
            await BaseRepository.InsertAsync(entity);
            var result = MapEntityToDto(entity);
            return result;
        }

        public Task<TDto> InsertManyAsync(List<TInsertDto> insertDtos)
        {
            throw new NotImplementedException();
        }

        public async Task<TDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await BaseRepository.GetByIdAsync(id);
            var newEntity = MapUpdateDtoToEntity(updateDto, entity);
            await ValidateUpdateBusiness(newEntity);
            await BaseRepository.UpdateAsync(newEntity, connection);
            var result = MapEntityToDto(newEntity);
            return result;
        }

        public Task<TDto> UpdateManyAsync(List<TUpdateDto> updateDtos)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await BaseRepository.GetByIdAsync(id);
            var result = await BaseRepository.DeleteAsync(entity);
            return result;
        }

        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            var entities = await BaseRepository.GetByListIdAsync(ids);
            var result = await BaseRepository.DeleteManyAsync(entities);
            return result;
        }

        public abstract TEntity MapInsertDtoToEntity(TInsertDto insertDto);

        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity);

        public virtual async Task ValidateInsertBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

        public virtual async Task ValidateUpdateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

        public virtual async Task ValidateDeleteBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }
    }
}
