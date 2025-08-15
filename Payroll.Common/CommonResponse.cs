using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common
{
    public class CommonResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }

        public static CommonResponse Success(string message, object? data = null) => new()
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };

        public static CommonResponse Failure(string message) => new()
        {
            IsSuccess = false,
            Message = message
        };
    }

    public class CommonResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public static CommonResult Success(object? data, string message = "Success") => new()
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };

        public static CommonResult Failure(string message) => new()
        {
            IsSuccess = false,
            Message = message
        };
    }
}
