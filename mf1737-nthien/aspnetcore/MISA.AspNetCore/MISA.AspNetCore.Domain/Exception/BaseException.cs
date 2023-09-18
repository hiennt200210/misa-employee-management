using System.Text.Json;

namespace MISA.AspNetCore.Domain
{
    public class BaseException
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
