namespace MISA.AspNetCore.Domain
{
    public class NotFoundException : System.Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
        #endregion

        #region Constructors
        public NotFoundException()
        {
            ErrorCode = ErrorCode.NotFound;
        }

        public NotFoundException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public NotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCode.NotFound;
        }

        public NotFoundException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
