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
        public BaseService(IBaseRepository<TEntity> baseRepository) : base(baseRepository)
        {
        }

        public async Task<int> InsertAsync(TInsertDto insertDto)
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
            var result = await BaseRepository.InsertAsync(entity);
            return result;
        }

        public async Task<int> InsertManyAsync(List<TInsertDto> insertDtos)
        {
            var entities = insertDtos.Select(insertDto => MapInsertDtoToEntity(insertDto)).ToList();

            for (var i = 0; i < entities.Count; i++)
            {
                if (entities[i].GetId() == Guid.Empty)
                {
                    entities[i].SetId(Guid.NewGuid());
                }

                if (entities[i] is BaseEntity baseEntity)
                {
                    baseEntity.CreatedDate = DateTime.Now;
                    baseEntity.CreatedBy ??= "hiennt200210";
                    baseEntity.ModifiedDate = DateTime.Now;
                    baseEntity.ModifiedBy ??= "hiennt200210";
                }

                await ValidateInsertBusiness(entities[i]);
            }

            var result = await BaseRepository.InsertManyAsync(entities);
            return result;
        }

        public async Task<int> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await BaseRepository.GetByIdAsync(id);
            var newEntity = MapUpdateDtoToEntity(updateDto, entity);
            await ValidateUpdateBusiness(newEntity);
            var result = await BaseRepository.UpdateAsync(newEntity);
            return result;
        }

        public async Task<int> UpdateManyAsync(List<TUpdateDto> updateDtos)
        {
            var entities = updateDtos.Select(updateDto => MapUpdateDtoToEntity(updateDto)).ToList();
            
            for (var i = 0; i < entities.Count; i++)
            {
                await ValidateUpdateBusiness(entities[i]);
            }
            
            var result = await BaseRepository.UpdateManyAsync(entities);
            return result;
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

        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto);

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
