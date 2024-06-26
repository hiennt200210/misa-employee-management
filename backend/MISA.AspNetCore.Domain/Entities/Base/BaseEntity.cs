﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi
        /// /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Sửa đổi bởi
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
