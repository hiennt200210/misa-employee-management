using Dapper;
using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AspNetCore.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : TEntity
    {
        protected readonly IDbConnectionService _dbConnectionService;

        public BaseRepository(IDbConnectionService dbConnectionService)
        {
            _dbConnectionService = dbConnectionService;
        }

        public virtual string TableName { get; set; } = typeof(TEntity).Name;

        public async Task<int> DeleteAsync(IEntity entity)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", entity.GetId());

            // Thực thi câu truy vấn
            var result = await connection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<int> DeleteManyAsync(List<IEntity> entities)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", entities.Select(entity => entity.GetId()));

            // Thực thi câu truy vấn
            var result = await connection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName}";

            // Thực thi câu truy vấn
            var result = await connection.QueryAsync<TEntity>(sql);

            return result.ToList();
        }

        public async Task<TEntity> GetByIdAsync(Guid entityId)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", entityId);

            // Thực thi câu truy vấn
            var result = await connection.QuerySingleAsync<TEntity>(sql, parameters);

            if (result == null)
            {
                throw new NotFoundException("Không tìm thấy bản ghi.");
            }

            return result;
        }

        public Task<List<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertManyAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateManyAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
