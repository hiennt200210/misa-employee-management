using System;

namespace MISA.AspNetCore.Domain
{
    public class ConflictException : System.Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
        #endregion

        #region Constructors
        public ConflictException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public ConflictException(string message) : base(message) { }

        public ConflictException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
