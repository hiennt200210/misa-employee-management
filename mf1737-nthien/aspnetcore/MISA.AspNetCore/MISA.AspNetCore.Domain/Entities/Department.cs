using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AspNetCore.Domain.Entities.Base;

namespace MISA.AspNetCore.Domain
{
    public class Department : BaseEntity, TEntity
    {
        /// <summary>
        /// Định danh của phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
    }
}
