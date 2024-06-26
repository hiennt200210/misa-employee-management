﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class BaseException
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

        /// <summary>
        /// Override phương thức ToString() để trả về chuỗi JSON
        /// </summary>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

