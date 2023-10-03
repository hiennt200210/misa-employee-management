using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Mã lỗi nghiệp vụ
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho lập trình viên
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// TraceId
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public object? Errors { get; set; }

        /// <summary>
        /// Thông tin bổ sung
        /// </summary>
        public string? MoreInfo { get; set; }
    }
}
