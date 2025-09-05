using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Enums
{
    public static class AppEnums
    {
        public enum RecordStatus
        {
            Inactive = 0,
            Active = 1,
            Deleted = -1
        }
    }
}
