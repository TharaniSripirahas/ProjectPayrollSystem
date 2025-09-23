using AdminService.Core.Interfaces;
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

namespace AdminService.Infrastructure.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly PayrollDbContext _context;

        public LeaveRequestService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequestDto>> GetAllAsync()
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .Select(x => new LeaveRequestDto
                {
                    LeaveId = x.LeaveId,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                    LeaveTypeId = x.LeaveTypeId,
                    LeaveTypeName = x.LeaveType.LeaveName,
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
            var x = await _context.LeaveRequests
                .Include(e => e.Employee)
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(e => e.LeaveId == id);

            if (x == null) return null;

            return new LeaveRequestDto
            {
                LeaveId = x.LeaveId,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                LeaveTypeId = x.LeaveTypeId,
                LeaveTypeName = x.LeaveType.LeaveName,
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

        public async Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto dto)
        {
            var entity = new LeaveRequest
            {
                EmployeeId = dto.EmployeeId,
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

            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.LeaveType).LoadAsync();

            return new LeaveRequestDto
            {
                LeaveId = entity.LeaveId,
                EmployeeId = entity.EmployeeId,
                EmployeeName = entity.Employee.FirstName + " " + entity.Employee.LastName,
                LeaveTypeId = entity.LeaveTypeId,
                LeaveTypeName = entity.LeaveType.LeaveName,
                StartDate = entity.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = entity.EndDate.ToDateTime(TimeOnly.MinValue),
                DaysCount = entity.DaysCount,
                Reason = entity.Reason,
                Status = entity.Status,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                RecordStatus = (RecordStatus)entity.RecordStatus
            };
        }

        public async Task<LeaveRequestDto?> UpdateAsync(long id, LeaveRequestCreateDto dto)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return null;

            if (dto.EmployeeId <= 0)
                throw new Exception("Invalid EmployeeId.");

            if (dto.LeaveTypeId <= 0)
                throw new Exception("Invalid LeaveTypeId.");

            var employeeExists = await _context.Employees.AnyAsync(e => e.EmployeeId == dto.EmployeeId);
            if (!employeeExists)
                throw new Exception($"EmployeeId {dto.EmployeeId} does not exist.");

            var leaveTypeExists = await _context.LeaveTypes.AnyAsync(l => l.LeaveTypeId == dto.LeaveTypeId);
            if (!leaveTypeExists)
                throw new Exception($"LeaveTypeId {dto.LeaveTypeId} does not exist.");

            entity.EmployeeId = dto.EmployeeId;
            entity.LeaveTypeId = dto.LeaveTypeId;
            entity.StartDate = DateOnly.FromDateTime(dto.StartDate);
            entity.EndDate = DateOnly.FromDateTime(dto.EndDate);
            entity.DaysCount = dto.DaysCount;
            entity.Reason = dto.Reason;
            entity.Status = dto.Status;
            if (dto.LastModifiedBy.HasValue)
                entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = (int)dto.RecordStatus;

            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.LeaveType).LoadAsync();

            return new LeaveRequestDto
            {
                LeaveId = entity.LeaveId,
                EmployeeId = entity.EmployeeId,
                EmployeeName = entity.Employee.FirstName + " " + entity.Employee.LastName,
                LeaveTypeId = entity.LeaveTypeId,
                LeaveTypeName = entity.LeaveType.LeaveName,
                StartDate = entity.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = entity.EndDate.ToDateTime(TimeOnly.MinValue),
                DaysCount = entity.DaysCount,
                Reason = entity.Reason,
                Status = entity.Status,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = (RecordStatus)entity.RecordStatus
            };
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
