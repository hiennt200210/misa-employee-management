namespace DemoAspNetCore
{
    public class ErrorService
    {
        /// <summary>
        /// Định danh mã nội bộ
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho Developer
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho User
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết về sự cố
        /// </summary>
        public object? MoreInfo { get; set; }

        /// <summary>
        /// Mã tra cứu thông tin log
        /// </summary>
        public string? TraceId { get; set; }
    }
}
