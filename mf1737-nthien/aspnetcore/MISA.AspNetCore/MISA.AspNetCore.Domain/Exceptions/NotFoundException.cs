using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class NotFoundException : BaseException
    {
        #region Constructors

        public NotFoundException(string userMessage) : base(ErrorCode.NotFound, userMessage)
        {
        }

        public NotFoundException(string devMessage, string userMessage, string errors, string traceId, string moreInfo) : base(ErrorCode.Conflict, devMessage, userMessage, errors, traceId, moreInfo)
        {
        }
        #endregion
    }
}

