using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;

namespace MISA.AspNetCore.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TDto, TInsertDto, TUpdateDto> : BaseReadOnlyController<TDto>
    {
        protected readonly IBaseService<TDto, TInsertDto, TUpdateDto> BaseService;
        public BaseController(IBaseService<TDto, TInsertDto, TUpdateDto> baseService) : base(baseService)
        {
            BaseService = baseService;
        }

        [HttpGet("{id}")]
        public async Task<EmployeeDto> GetByIdAsync(Guid id)
        {
            var result = await BaseService.GetByIdAsync(id);
            return result;
        }

        [HttpPost]
        public async Task<int> InsertAsync(EmployeeInsertDto insertDto)
        {
            var result = await BaseService.InsertAsync(insertDto);
            return result;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<int> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var result = await BaseService.UpdateAsync(id, updateDto);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await BaseService.DeleteAsync(id);
            return result;
        }
    }
}
