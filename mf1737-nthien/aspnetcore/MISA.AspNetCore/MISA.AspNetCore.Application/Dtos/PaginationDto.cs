using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class PaginationDto
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Số lượng bản ghi trên một trang
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Số lượng bản ghi trên một trang
        /// </summary>
        public int Limit { get; set; }
        
        /// <summary>
        /// Vị trí bắt đầu lấy dữ liệu
        /// </summary>
        public int Offset { get; set; }
        
        /// <summary>
        /// Dữ liệu
        /// </summary>
        public List<object>? Data { get; set; }
    }
}
