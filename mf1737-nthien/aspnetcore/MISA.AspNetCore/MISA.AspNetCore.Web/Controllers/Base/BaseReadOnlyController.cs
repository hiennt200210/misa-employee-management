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

        [HttpGet]
        public async Task<List<TDto>> GetAllAsync()
        {
            var result = await BaseReadOnlyService.GetAllAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var result = await BaseReadOnlyService.GetByIdAsync(id);
            return result;
        }
    }
}
