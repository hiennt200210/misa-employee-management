using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class DepartmentInsertDto : BaseDto
    {
        #region Properties
        
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string? DepartmentName { get; set; }

        #endregion
    }
}
