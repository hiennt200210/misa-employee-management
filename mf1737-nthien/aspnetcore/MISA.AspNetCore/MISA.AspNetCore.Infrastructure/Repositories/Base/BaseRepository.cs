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
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
    {
        protected readonly IDbConnectionService DbConnectionService;

        public BaseRepository(IDbConnectionService dbConnectionService)
        {
            DbConnectionService = dbConnectionService;
        }

        public virtual string TableName { get; set; } = typeof(TEntity).Name;

        public async Task<int> DeleteAsync(Guid id)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id IN (";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();

            for (int i = 0; i < ids.Count; i++)
            {
                sql += $"@id{i},";
                parameters.Add($"@id{i}", ids[i]);
            }

            sql += ");";

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName}";

            // Thực thi câu truy vấn
            var result = await DbConnection.QueryAsync<TEntity>(sql);

            return result.ToList();
        }

        public async Task<TEntity> GetByIdAsync(Guid entityId)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", entityId);

            // Thực thi câu truy vấn
            var result = await DbConnection.QuerySingleAsync<TEntity>(sql, parameters);

            if (result == null)
            {
                throw new NotFoundException("Không tìm thấy bản ghi.");
            }

            return result;
        }

        public async Task<List<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

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

            sql += ");";

            // Thực thi câu truy vấn
            var result = await DbConnection.QueryAsync<TEntity>(sql, parameters);

            if (result == null)
            {
                throw new NotFoundException("Không tìm thấy bản ghi.");
            }

            return result.ToList();
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

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
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<int> InsertManyAsync(List<TEntity> entities)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"INSERT INTO {TableName} (";
            var parameters = new DynamicParameters();
            foreach (var entity in entities)
            {
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
                foreach (var property in properties)
                {
                    // Lấy ra tên của property
                    var propertyName = property.Name;

                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);

                    // Thêm parameter
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"UPDATE {TableName} SET ";

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            // Lấy ra id của object
            var entityId = id;

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
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            return result;
        }

        public async Task<int> UpdateManyAsync(List<Guid> ids, List<TEntity> entities)
        {
            // Khởi tạo đối tượng kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"UPDATE {TableName} SET ";

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();

            for (var i = 0; i < entities.Count; i++)
            {
                var propertyName = properties[i].Name;
                var propertyValue = properties[i].GetValue(entities[i]);
                parameters.Add($"@{propertyName}", propertyValue);
            }

            sql += $" WHERE {TableName}Id = @{TableName}Id;";
            
            for (var i = 0; i < ids.Count; i++)
            {
                parameters.Add($"@{TableName}Id", ids[i]);
            }

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);
            return result;
        }
    }
}
