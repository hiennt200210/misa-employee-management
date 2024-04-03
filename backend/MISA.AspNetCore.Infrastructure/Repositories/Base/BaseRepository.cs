using Dapper;
using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace MISA.AspNetCore.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly IDbConnectionService DbConnectionService;
        protected virtual string TableName { get; set; } = typeof(TEntity).Name;

        public BaseRepository(IDbConnectionService dbConnectionService)
        {
            DbConnectionService = dbConnectionService;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi nào</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<List<TEntity>> GetAllAsync()
        {
            // Tạo kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName} ORDER BY CreatedDate";

            // Thực thi câu truy vấn
            var result = await DbConnection.QueryAsync<TEntity>(sql);

            if (result == null)
            {
                throw new NotFoundException()
                {
                    ErrorCode = ErrorCode.NotFound,
                    DevMessage = Domain.Resources.Errors.NotFound,
                    UserMessage = Domain.Resources.Errors.NotFound,
                    MoreInfo = "",
                    TraceId = "",
                    Errors = new List<string>() { Domain.Resources.Errors.NotFound }
                };
            }

            return result.ToList();
        }

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần lấy</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            // Tạo kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            // Thực thi câu truy vấn
            var result = await DbConnection.QuerySingleOrDefaultAsync<TEntity>(sql, parameters);

            if (result == null)
            {
                throw new NotFoundException();
            }

            return result;
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm mới</param>
        /// <returns>1 (Thêm mới thành công)</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            // Tạo kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"INSERT INTO {TableName} (";

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                // Lấy ra tên của property
                var propertyName = properties[i].Name;

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
                // Lấy ra tên của property
                var propertyName = properties[i].Name;

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

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần cập nhật</param>
        /// <param name="entity">Bản ghi mới cần cập nhật</param>
        /// <returns>1 (Cập nhật thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần cập nhật</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            // Tạo kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            // Tạo câu truy vấn
            var sql = $"UPDATE {TableName} SET ";

            // Lấy ra các property của object
            var properties = typeof(TEntity).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                // Lấy ra tên của property
                var propertyName = properties[i].Name;

                // Lấy ra giá trị của property
                var propertyValue = properties[i].GetValue(entity);

                // Nếu là property đầu tiên thì không cần thêm dấu phẩy
                if (i == 0)
                {
                    sql += $"{propertyName} = @{propertyName}";
                    parameters.Add($"@{propertyName}", propertyValue);
                }
                else
                {
                    sql += $", {propertyName} = @{propertyName}";
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            sql += $" WHERE {TableName}Id = @id;";

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            return result;
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần xóa</param>
        /// <returns>1 (Xóa thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần xóa</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            // Tạo kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";

            // Tạo dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            if (result == 0)
            {
                throw new NotFoundException();
            }

            return result;
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách định danh của các bản ghi cần xóa</param>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần xóa</exception>"
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// CreatedBy: hiennt200210 (25/09/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            // Tạo kết nối với database
            var DbConnection = DbConnectionService.GetConnection();

            // Tạo câu truy vấn và dynamic parameter
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id IN (";
            var parameters = new DynamicParameters();

            for (int i = 0; i < ids.Count; i++)
            {
                // Nếu là id đầu tiên thì không cần thêm dấu phẩy
                if (i == 0)
                {
                    sql += $"@ids{i}";
                }
                else
                {
                    sql += $", @ids{i}";
                }
                parameters.Add($"@ids{i}", ids[i]);
            }

            sql += ");";

            // Thực thi câu truy vấn
            var result = await DbConnection.ExecuteAsync(sql, parameters);

            if (result < ids.Count)
            {
                throw new NotFoundException();
            }

            return result;
        }
    }
}
