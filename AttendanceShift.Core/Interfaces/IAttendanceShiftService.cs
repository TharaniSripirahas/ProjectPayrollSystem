using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceShift.Core.Interfaces
{
    public interface IAttendanceLogService
    {
        Task<ApiResult<AttendanceLogDto>> GetAllAsync();
        Task<ApiResult<AttendanceLogDto>> GetByIdAsync(long id);
        Task<ApiResult<AttendanceLogDto>> CreateAsync(AttendanceLogCreateDto dto);
        Task<ApiResult<AttendanceLogDto>> UpdateAsync(long id, AttendanceLogDto dto);
        Task<ApiResult<bool>> DeleteAsync(long id);
    }
}
