using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class DepartmentUpdateDto : BaseDto
    {
        #region Properties

        /// <summary>
        /// Định danh của phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string? DepartmentName { get; set; }

        #endregion
    }
}
