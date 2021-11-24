using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.BaseModel
{

    public class BusinessException : Exception
    {
        public int ErrorCode { get; }
        public BusinessException(string errorDesc, int errorCode)
            : base(errorDesc)
        {
            ErrorCode = errorCode;
        }
    }
}
