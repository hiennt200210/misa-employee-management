using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA.AspNetCore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseReadOnlyController<DepartmentDto>
    {
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
        }
    }
}
