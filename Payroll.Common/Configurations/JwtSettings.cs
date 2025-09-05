using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Configurations
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public int ExpiryMinutes { get; set; } = 60;
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
