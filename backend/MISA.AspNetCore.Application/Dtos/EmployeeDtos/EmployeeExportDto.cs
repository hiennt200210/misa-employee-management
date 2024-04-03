using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeExportDto
    {
        /// <summary>
        /// File dữ liệu
        /// </summary>
        public byte[]? File { get; set; }

        /// <summary>
        /// MIME type
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        public string? FileName { get; set; }
    }
}