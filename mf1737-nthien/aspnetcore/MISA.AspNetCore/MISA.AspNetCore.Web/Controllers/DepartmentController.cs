using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;

namespace MISA.AspNetCore.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            return departments;
        }

        [HttpGet("{departmentId}")]
        public async Task<DepartmentModel> GetByIdAsync(Guid departmentId)
        {
            var department = await _departmentService.GetByIdAsync(departmentId);
            return department;
        }

        [HttpPost]
        public async Task<int> InsertAsync(DepartmentInsertModel departmentInsertModel)
        {
            var affectedRows = await _departmentService.InsertAsync(departmentInsertModel);
            return affectedRows;
        }

        [HttpPut("{department}")]
        public async Task<int> UpdateAsync(DepartmentUpdateModel departmentUpdateModel)
        {
            var affectedRows = await _departmentService.UpdateAsync(departmentUpdateModel);
            return affectedRows;
        }

        [HttpDelete("{departmentId}")]
        public async Task<int> DeleteAsync(Guid departmentId)
        {
            var affectedRows = await _departmentService.DeleteAsync(departmentId);
            return affectedRows;
        }
    }
}
