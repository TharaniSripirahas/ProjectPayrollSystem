using AttendanceShift.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
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
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly DbContextPayrollProject _context;

        public LeaveRequestService(DbContextPayrollProject context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequestDto>> GetAllAsync()
        {
            return await _context.LeaveRequests
                .Select(x => new LeaveRequestDto
                {
                    LeaveId = x.LeaveId,
                    EmployeeName = x.EmployeeName,
                    LeaveTypeId = x.LeaveTypeId,
                    StartDate = x.StartDate.ToDateTime(TimeOnly.MinValue),
                    EndDate = x.EndDate.ToDateTime(TimeOnly.MinValue),
                    DaysCount = x.DaysCount,
                    Reason = x.Reason,
                    Status = x.Status,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    RecordStatus = (RecordStatus)x.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<LeaveRequestDto?> GetByIdAsync(long id)
        {
            var x = await _context.LeaveRequests.FindAsync(id);
            if (x == null) return null;

            return new LeaveRequestDto
            {
                LeaveId = x.LeaveId,
                EmployeeName = x.EmployeeName,
                LeaveTypeId = x.LeaveTypeId,
                StartDate = x.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = x.EndDate.ToDateTime(TimeOnly.MinValue),
                DaysCount = x.DaysCount,
                Reason = x.Reason,
                Status = x.Status,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                LastModifiedBy = x.LastModifiedBy,
                LastModifiedOn = x.LastModifiedOn,
                RecordStatus = (RecordStatus)x.RecordStatus
            };
        }

        public async Task<LeaveRequestDto> CreateAsync(LeaveRequestDto dto)
        {
            var entity = new LeaveRequest
            {
                EmployeeName = dto.EmployeeName,
                LeaveTypeId = dto.LeaveTypeId,
                StartDate = DateOnly.FromDateTime(dto.StartDate),
                EndDate = DateOnly.FromDateTime(dto.EndDate),
                DaysCount = dto.DaysCount,
                Reason = dto.Reason,
                Status = dto.Status,
                CreatedBy = dto.CreatedBy ?? 0,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = (int)dto.RecordStatus
            };

            _context.LeaveRequests.Add(entity);
            await _context.SaveChangesAsync();

            dto.LeaveId = entity.LeaveId;
            dto.CreatedOn = entity.CreatedOn;
            return dto;
        }

        public async Task<LeaveRequestDto?> UpdateAsync(long id, LeaveRequestDto dto)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return null;

            entity.EmployeeName = dto.EmployeeName;
            entity.LeaveTypeId = dto.LeaveTypeId;
            entity.StartDate = DateOnly.FromDateTime(dto.StartDate);
            entity.EndDate = DateOnly.FromDateTime(dto.EndDate);
            entity.DaysCount = dto.DaysCount;
            entity.Reason = dto.Reason;
            entity.Status = dto.Status;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = (int)dto.RecordStatus;

            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return false;

            _context.LeaveRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
