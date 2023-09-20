using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AspNetCore.Application;

namespace MISA.AspNetCore.Web.Controllers
{
    public class EmployeeController : BaseController<EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>
    {
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
        }
    }
}
