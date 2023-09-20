using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class BaseException : Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
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
        public string? Errors { get; set; }

        /// <summary>
        /// Thông tin bổ sung
        /// </summary>
        public string? MoreInfo { get; set; }
        #endregion

        #region Constructors
        public BaseException(ErrorCode errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public BaseException(ErrorCode errorCode, string userMessage) : base()
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
        }

        public BaseException(ErrorCode errorCode, string devMessage, string userMessage, string errors, string traceId, string moreInfo) : base()
        {
            ErrorCode = errorCode;
            DevMessage = devMessage;
            UserMessage = userMessage;
            Errors = errors;
            TraceId = traceId;
            MoreInfo = moreInfo;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Override phương thức ToString() để trả về chuỗi JSON
        /// </summary>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}

