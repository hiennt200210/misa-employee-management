using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class DepartmentService : BaseReadOnlyService<Department, DepartmentDto>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
        }

        public override DepartmentDto MapEntityToDto(Department entity)
        {
            var departmentDto = new DepartmentDto()
            {
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.DepartmentName
            };

            return departmentDto;
        }
    }
}
