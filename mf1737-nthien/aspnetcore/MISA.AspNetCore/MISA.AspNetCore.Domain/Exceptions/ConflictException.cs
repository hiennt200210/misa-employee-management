using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class ConflictException : BaseException
    {
        #region Constructors

        public ConflictException(string userMessage) : base(ErrorCode.Conflict, userMessage)
        {
        }

        public ConflictException(string devMessage, string userMessage, string errors, string traceId, string moreInfo) : base(ErrorCode.Conflict, devMessage, userMessage, errors, traceId, moreInfo)
        {
        }
        #endregion
    }
}
