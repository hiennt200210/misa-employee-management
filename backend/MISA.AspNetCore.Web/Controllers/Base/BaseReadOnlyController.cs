using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;

namespace MISA.AspNetCore.Web
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseReadOnlyController<TDto> : ControllerBase
    {
        protected readonly IBaseReadOnlyService<TDto> BaseReadOnlyService;

        public BaseReadOnlyController(IBaseReadOnlyService<TDto> baseReadOnlyService)
        {
            BaseReadOnlyService = baseReadOnlyService;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi nào</exception>"
        /// CreatedBy: hiennt200210 (16/09/2023)
        [HttpGet]
        public async Task<List<TDto>> GetAllAsync()
        {
            var result = await BaseReadOnlyService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="id">Định danh của bản ghi cần lấy</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi cần lấy</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        [HttpGet]
        [Route("{id}")]
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var result = await BaseReadOnlyService.GetByIdAsync(id);
            return result;
        }
    }
}
