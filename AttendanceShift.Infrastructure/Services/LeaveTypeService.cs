using AttendanceShift.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payroll.Common.NonEntities;
using static Payroll.Common.Enums.AppEnums;

namespace AttendanceShift.Infrastructure.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly DbContextPayrollProject _context;

        public LeaveTypeService(DbContextPayrollProject context)
        {
            _context = context;
        }

        public async Task<List<LeaveTypeDto>> GetAllAsync()
        {
            return await _context.LeaveTypes
                .Select(l => new LeaveTypeDto
                {
                    LeaveTypeId = l.LeaveTypeId,
                    LeaveName = l.LeaveName,
                    IsPaid = l.IsPaid,
                    MaxDaysPerYear = l.MaxDaysPerYear,
                    CreatedBy = l.CreatedBy,
                    CreatedOn = l.CreatedOn,
                    LastModifiedBy = l.LastModifiedBy,
                    LastModifiedOn = l.LastModifiedOn,
                    RecordStatus = (RecordStatus)l.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<LeaveTypeDto?> GetByIdAsync(long id)
        {
            return await _context.LeaveTypes
                .Where(l => l.LeaveTypeId == id)
                .Select(l => new LeaveTypeDto
                {
                    LeaveTypeId = l.LeaveTypeId,
                    LeaveName = l.LeaveName,
                    IsPaid = l.IsPaid,
                    MaxDaysPerYear = l.MaxDaysPerYear,
                    CreatedBy = l.CreatedBy,
                    CreatedOn = l.CreatedOn,
                    LastModifiedBy = l.LastModifiedBy,
                    LastModifiedOn = l.LastModifiedOn,
                    RecordStatus = (RecordStatus)l.RecordStatus
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LeaveTypeDto> CreateAsync(LeaveTypeDto dto)
        {
            var now = DateTime.UtcNow;

            var entity = new LeaveType
            {
                LeaveName = dto.LeaveName,
                IsPaid = dto.IsPaid,
                MaxDaysPerYear = dto.MaxDaysPerYear,
                CreatedBy = dto.CreatedBy,
                CreatedOn = now,
                LastModifiedBy = dto.CreatedBy,
                LastModifiedOn = now,
                RecordStatus = (int)dto.RecordStatus
            };

            _context.LeaveTypes.Add(entity);
            await _context.SaveChangesAsync();

            dto.LeaveTypeId = entity.LeaveTypeId;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedOn = entity.LastModifiedOn;

            return dto;
        }

        public async Task<LeaveTypeDto?> UpdateAsync(long id, LeaveTypeDto dto)
        {
            var entity = await _context.LeaveTypes.FindAsync(id);
            if (entity == null) return null;

            entity.LeaveName = dto.LeaveName;
            entity.IsPaid = dto.IsPaid;
            entity.MaxDaysPerYear = dto.MaxDaysPerYear;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = (int)dto.RecordStatus;

            await _context.SaveChangesAsync();

            dto.LastModifiedOn = entity.LastModifiedOn;
            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.LeaveTypes.FindAsync(id);
            if (entity == null) return false;

            _context.LeaveTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
