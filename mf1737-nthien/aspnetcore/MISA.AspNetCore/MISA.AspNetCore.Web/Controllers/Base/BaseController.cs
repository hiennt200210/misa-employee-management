using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;
using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Infrastructure;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using System.Data;
using System.Drawing;

namespace MISA.AspNetCore.Web
{
    public class BaseController<TDto, TInsertDto, TUpdateDto> : BaseReadOnlyController<TDto>
    {
        protected readonly IBaseService<TDto, TInsertDto, TUpdateDto> BaseService;

        public BaseController(IBaseService<TDto, TInsertDto, TUpdateDto> baseService) : base(baseService)
        {
            BaseService = baseService;
        }

        /// <summary>
        /// Thêm mới một đối tượng
        /// </summary>
        /// <param name="insertDto">Đối tượng cần thêm mới</param>
        /// <returns>1 (Thêm mới thành công)</returns>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        [HttpPost]
        public async Task<IActionResult> InsertAsync(TInsertDto insertDto)
        {
            var result = await BaseService.InsertAsync(insertDto);
            return StatusCode(201, result);
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
        [HttpPut]
        [Route("{id}")]
        public async Task<int> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var result = await BaseService.UpdateAsync(id, updateDto);
            return result;
        }

        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="id">Định danh của đối tượng cần xóa</param>
        /// <returns>1 (Xoá thành công)</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng cần xoá</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        [HttpDelete]
        [Route("{id}")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await BaseService.DeleteAsync(id);
            return result;
        }

        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="ids">Danh sách định danh của các đối tượng cần xóa</param>
        /// <returns>Số lượng đối tượng đã xóa</returns>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [HttpDelete]
        public async Task<int> DeleteMultipleAsync([FromQuery] List<Guid> ids)
        {
            var result = await BaseService.DeleteMultipleAsync(ids);
            return result;
        }
    }
}