using AttendanceShift.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Enums;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.Enums.AppEnums;

namespace AttendanceShift.Infrastructure.Services
{
    public class AttendanceLogService : IAttendanceLogService
    {
        private readonly PayrollDbContext _context;

        public AttendanceLogService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<AttendanceLogDto>> GetAllAsync()
        {
            var result = new ApiResult<AttendanceLogDto>();

            try
            {
                var logs = await _context.AttendanceLogs
                    .Include(x => x.Employee)
                    .Include(x => x.Shift)
                    .Select(x => new AttendanceLogDto
                    {
                        AttendanceId = x.AttendanceId,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                        LogDate = x.LogDate,
                        PunchIn = x.PunchIn,
                        PunchOut = x.PunchOut,
                        ShiftId = x.ShiftId,
                        ShiftName = x.Shift.ShiftName,
                        IsLate = x.IsLate,
                        LateMinutes = x.LateMinutes,
                        EarlyDepartureMinutes = x.EarlyDepartureMinutes,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        LastModifiedBy = x.LastModifiedBy,
                        LastModifiedOn = x.LastModifiedOn,
                        RecordStatus = (RecordStatus)x.RecordStatus
                    })
                    .ToListAsync();

                result.ResponseCode = 1;
                result.Message = "Attendance logs retrieved successfully";
                result.ResponseData = logs;
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error retrieving attendance logs";
                result.ErrorDesc = ex.Message;
            }

            return result;
        }

        public async Task<ApiResult<AttendanceLogDto>> GetByIdAsync(long id)
        {
            var result = new ApiResult<AttendanceLogDto>();

            var log = await _context.AttendanceLogs
                .Include(x => x.Employee)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.AttendanceId == id);

            if (log == null)
            {
                result.ResponseCode = 0;
                result.Message = "Attendance log not found";
                return result;
            }

            var dto = new AttendanceLogDto
            {
                AttendanceId = log.AttendanceId,
                EmployeeId = log.EmployeeId,
                EmployeeName = log.Employee.FirstName + " " + log.Employee.LastName,
                LogDate = log.LogDate,
                PunchIn = log.PunchIn,
                PunchOut = log.PunchOut,
                ShiftId = log.ShiftId,
                ShiftName = log.Shift.ShiftName,
                IsLate = log.IsLate,
                LateMinutes = log.LateMinutes,
                EarlyDepartureMinutes = log.EarlyDepartureMinutes,
                CreatedBy = log.CreatedBy,
                CreatedOn = log.CreatedOn,
                LastModifiedBy = log.LastModifiedBy,
                LastModifiedOn = log.LastModifiedOn,
                RecordStatus = (RecordStatus)log.RecordStatus
            };

            result.ResponseCode = 1;
            result.Message = "Attendance log retrieved successfully";
            result.ResponseData.Add(dto);

            return result;
        }

        public async Task<ApiResult<AttendanceLogDto>> CreateAsync(AttendanceLogCreateDto dto)
        {
            var result = new ApiResult<AttendanceLogDto>();

            try
            {
                var log = new AttendanceLog
                {
                    EmployeeId = dto.EmployeeId,
                    LogDate = dto.LogDate,
                    PunchIn = dto.PunchIn,
                    PunchOut = dto.PunchOut,
                    ShiftId = dto.ShiftId,
                    IsLate = dto.IsLate,
                    LateMinutes = dto.LateMinutes,
                    EarlyDepartureMinutes = dto.EarlyDepartureMinutes,
                    CreatedBy = dto.CreatedBy ?? 0,
                    CreatedOn = DateTime.UtcNow,
                    RecordStatus = (int)dto.RecordStatus
                };

                _context.AttendanceLogs.Add(log);
                await _context.SaveChangesAsync();

                await _context.Entry(log).Reference(x => x.Employee).LoadAsync();
                await _context.Entry(log).Reference(x => x.Shift).LoadAsync();

                var createdDto = new AttendanceLogDto
                {
                    AttendanceId = log.AttendanceId,
                    EmployeeId = log.EmployeeId,
                    EmployeeName = log.Employee.FirstName + " " + log.Employee.LastName,
                    LogDate = log.LogDate,
                    PunchIn = log.PunchIn,
                    PunchOut = log.PunchOut,
                    ShiftId = log.ShiftId,
                    ShiftName = log.Shift.ShiftName,
                    IsLate = log.IsLate,
                    LateMinutes = log.LateMinutes,
                    EarlyDepartureMinutes = log.EarlyDepartureMinutes,
                    CreatedBy = log.CreatedBy,
                    CreatedOn = log.CreatedOn,
                    LastModifiedBy = log.LastModifiedBy,
                    LastModifiedOn = log.LastModifiedOn,
                    RecordStatus = (RecordStatus)log.RecordStatus
                };

                result.ResponseCode = 1;
                result.Message = "Attendance log created successfully";
                result.ResponseData.Add(createdDto);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error occurred while creating attendance log";
                result.ErrorDesc = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }


        public async Task<ApiResult<AttendanceLogDto>> UpdateAsync(long id, AttendanceLogDto dto)
        {
            var result = new ApiResult<AttendanceLogDto>();

            var log = await _context.AttendanceLogs.FindAsync(id);
            if (log == null)
            {
                result.ResponseCode = 0;
                result.Message = "Attendance log not found";
                return result;
            }

            try
            {
                log.PunchIn = dto.PunchIn;
                log.PunchOut = dto.PunchOut;
                log.IsLate = dto.IsLate;
                log.LateMinutes = dto.LateMinutes;
                log.EarlyDepartureMinutes = dto.EarlyDepartureMinutes;
                log.LastModifiedBy = dto.LastModifiedBy;
                log.LastModifiedOn = DateTime.UtcNow;
                log.RecordStatus = (int)dto.RecordStatus;

                await _context.SaveChangesAsync();

                result.ResponseCode = 1;
                result.Message = "Attendance log updated successfully";
                result.ResponseData.Add(dto);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error occurred while updating attendance log";
                result.ErrorDesc = ex.Message;
            }

            return result;
        }

        public async Task<ApiResult<bool>> DeleteAsync(long id)
        {
            var result = new ApiResult<bool>();

            var log = await _context.AttendanceLogs.FindAsync(id);
            if (log == null)
            {
                result.ResponseCode = 0;
                result.Message = "Attendance log not found";
                return result;
            }

            try
            {
                _context.AttendanceLogs.Remove(log);
                await _context.SaveChangesAsync();

                result.ResponseCode = 1;
                result.Message = "Attendance log deleted successfully";
                result.ResponseData.Add(true);
            }
            catch (Exception ex)
            {
                result.ResponseCode = 0;
                result.Message = "Error occurred while deleting attendance log";
                result.ErrorDesc = ex.Message;
            }

            return result;
        }
    }

}
