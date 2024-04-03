using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public enum ErrorCode
    {
        /// <summary>
        /// Yêu cầu không hợp lệ
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Chưa được xác thực
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Không có quyền truy cập
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// Không tìm thấy tài nguyên
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Tài nguyên bị trùng
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// Lỗi hệ thống
        /// </summary>
        InternalServerError = 500
    }
}

