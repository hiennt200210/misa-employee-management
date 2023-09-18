using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;

namespace MISA.AspNetCore.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<List<EmployeeModel>> GetAllAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            return employees;
		}

        [HttpGet("{employeeId}")]
        public async Task<EmployeeModel> GetByIdAsync(Guid employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            return employee;
        }

        [HttpPost]
        public async Task<int> InsertAsync(EmployeeInsertModel employeeInsertModel)
        {
            var affectedRows = await _employeeService.InsertAsync(employeeInsertModel);
            return affectedRows;
        }

        [HttpPut("{employee}")]
        public async Task<int> UpdateAsync(EmployeeUpdateModel employeeUpdateModel)
        {
            var affectedRows = await _employeeService.UpdateAsync(employeeUpdateModel);
            return affectedRows;
        }

        [HttpDelete("{employeeId}")]
        public async Task<int> DeleteAsync(Guid employeeId)
        {
            var affectedRows = await _employeeService.DeleteAsync(employeeId);
            return affectedRows;
        }
    }
}
