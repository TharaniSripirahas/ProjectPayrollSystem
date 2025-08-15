using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class ApiResult<T>
    {
        public ApiResult()
        {
            ResponseCode = -1;
            Message = string.Empty;
            ErrorDesc = string.Empty;
            ResponseData = new List<T>();
        }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public string ErrorDesc { get; set; }
        public List<T> ResponseData { get; set; }
    }
}
