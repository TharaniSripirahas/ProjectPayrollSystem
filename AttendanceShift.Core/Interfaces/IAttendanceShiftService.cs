using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceShift.Core.Interfaces
{
    public interface IShiftService
    {
        Task<List<ShiftDto>> GetAllAsync();
        Task<ShiftDto?> GetByIdAsync(long id);
        Task<ShiftDto> CreateAsync(ShiftDto dto);
        Task<ShiftDto?> UpdateAsync(long id, ShiftDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public interface IAttendanceLogService
    {
        Task<ApiResult<AttendanceLogDto>> GetAllAsync();
        Task<ApiResult<AttendanceLogDto>> GetByIdAsync(long id);
        Task<ApiResult<AttendanceLogDto>> CreateAsync(AttendanceLogDto dto);
        Task<ApiResult<AttendanceLogDto>> UpdateAsync(long id, AttendanceLogDto dto);
        Task<ApiResult<bool>> DeleteAsync(long id);
    }

    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeDto>> GetAllAsync();
        Task<LeaveTypeDto?> GetByIdAsync(long id);
        Task<LeaveTypeDto> CreateAsync(LeaveTypeDto dto);
        Task<LeaveTypeDto?> UpdateAsync(long id, LeaveTypeDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestDto>> GetAllAsync();
        Task<LeaveRequestDto?> GetByIdAsync(long id);
        Task<LeaveRequestDto> CreateAsync(LeaveRequestDto dto);
        Task<LeaveRequestDto?> UpdateAsync(long id, LeaveRequestDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
