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

        public Task<int> DeleteManyAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
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

        public async Task<List<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id IN (";

            for (int i = 0; i < ids.Count; i++)
            {
                sql += $"@id{i},";
            }

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();

            for (int i = 0; i < ids.Count; i++)
            {
                parameters.Add($"@id{i}", ids[i]);
            }

            // Thực thi câu truy vấn
            var result = await connection.QueryAsync<TEntity>(sql, parameters);

            if (result == null)
            {
                throw new NotFoundException("Không tìm thấy bản ghi.");
            }

            return result.ToList();
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"INSERT INTO {TableName} (";

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                // Lấy ra tên của property
                var propertyName = property.Name;

                // Nếu là property đầu tiên thì không cần thêm dấu phẩy
                if (i == 0)
                {
                    sql += $"{propertyName}";
                }
                else
                {
                    sql += $", {propertyName}";
                }
            }

            sql += ") VALUES (";

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                // Lấy ra tên của property
                var propertyName = property.Name;

                // Nếu là property đầu tiên thì không cần thêm dấu phẩy
                if (i == 0)
                {
                    sql += $"@{propertyName}";
                }
                else
                {
                    sql += $", @{propertyName}";
                }
            }

            sql += ");";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            
            foreach (var property in properties)
            {
                // Lấy ra tên của property
                var propertyName = property.Name;

                // Lấy ra giá trị của property
                var propertyValue = property.GetValue(entity);

                // Thêm parameter
                parameters.Add($"@{propertyName}", propertyValue);
            }

            // Thực thi câu truy vấn
            var result = connection.Execute(sql, parameters);

            return result;
        }

        public Task<int> InsertManyAsync(List<TEntity> entities)
        {
        }

        public async Task<int> UpdateAsync(TEntity entity, System.Data.IDbConnection connection)
        {
            // Khởi tạo đối tượng kết nối với database
            var connection = _dbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"UPDATE {TableName} SET ";

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            // Lấy ra id của object
            var entityId = entity.GetId();

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                // Lấy ra tên của property
                var propertyName = property.Name;

                // Lấy ra giá trị của property
                var propertyValue = property.GetValue(entity);

                // Nếu là property đầu tiên thì không cần thêm dấu phẩy
                if (property.Name == "EntityId")
                {
                    continue;
                }
                else if (property.Name == "ModifiedBy")
                {
                    sql += $"{propertyName} = @{propertyName}, ";
                    parameters.Add($"@{propertyName}", propertyValue);
                }
                else if (property.Name == "ModifiedDate")
                {
                    sql += $"{propertyName} = @{propertyName} ";
                    parameters.Add($"@{propertyName}", propertyValue);
                }
                else
                {
                    sql += $"{propertyName} = @{propertyName}, ";
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            // Thực thi câu truy vấn
            var result = await connection.Execute(sql, parameters);

            return result;
        }

        public Task<int> UpdateManyAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
