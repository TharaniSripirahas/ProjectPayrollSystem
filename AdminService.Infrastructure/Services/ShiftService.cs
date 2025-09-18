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
    public class ShiftService : IShiftService
        {
            private readonly PayrollDbContext _context;

            public ShiftService(PayrollDbContext context)
            {
                _context = context;
            }

            public async Task<List<ShiftDto>> GetAllAsync()
            {
                return await _context.Shifts
                    .Select(s => new ShiftDto
                    {
                        ShiftId = s.ShiftId,
                        ShiftName = s.ShiftName,
                        ShiftTime = s.ShiftTime,
                        EndTime = s.EndTime,
                        IsNightShift = s.IsNightShift,
                        IsWeekendShift = s.IsWeekendShift,
                        IsHolidayShift = s.IsHolidayShift,
                        AllowancePercentage = s.AllowancePercentage,
                        CreatedBy = s.CreatedBy,
                        CreatedOn = s.CreatedOn,
                        LastModifiedBy = s.LastModifiedBy,
                        LastModifiedOn = s.LastModifiedOn,
                        RecordStatus = (RecordStatus)s.RecordStatus
                    })
                    .ToListAsync();
            }

            public async Task<ShiftDto?> GetByIdAsync(long id)
            {
                return await _context.Shifts
                    .Where(s => s.ShiftId == id)
                    .Select(s => new ShiftDto
                    {
                        ShiftId = s.ShiftId,
                        ShiftName = s.ShiftName,
                        ShiftTime = s.ShiftTime,
                        EndTime = s.EndTime,
                        IsNightShift = s.IsNightShift,
                        IsWeekendShift = s.IsWeekendShift,
                        IsHolidayShift = s.IsHolidayShift,
                        AllowancePercentage = s.AllowancePercentage,
                        CreatedBy = s.CreatedBy,
                        CreatedOn = s.CreatedOn,
                        LastModifiedBy = s.LastModifiedBy,
                        LastModifiedOn = s.LastModifiedOn,
                        RecordStatus = (RecordStatus)s.RecordStatus
                    })
                    .FirstOrDefaultAsync();
            }

            public async Task<ShiftDto> CreateAsync(ShiftDto dto)
            {
                var entity = new Shift
                {
                    ShiftName = dto.ShiftName,
                    ShiftTime = dto.ShiftTime,
                    EndTime = dto.EndTime,
                    IsNightShift = dto.IsNightShift,
                    IsWeekendShift = dto.IsWeekendShift,
                    IsHolidayShift = dto.IsHolidayShift,
                    AllowancePercentage = dto.AllowancePercentage,
                    CreatedBy = dto.CreatedBy ?? 0,
                    CreatedOn = DateTime.UtcNow,
                    RecordStatus = (int)dto.RecordStatus
                };

                _context.Shifts.Add(entity);
                await _context.SaveChangesAsync();

                dto.ShiftId = entity.ShiftId;
                return dto;
            }

            public async Task<ShiftDto?> UpdateAsync(long id, ShiftDto dto)
            {
                var entity = await _context.Shifts.FindAsync(id);
                if (entity == null) return null;

                entity.ShiftName = dto.ShiftName;
                entity.ShiftTime = dto.ShiftTime;
                entity.EndTime = dto.EndTime;
                entity.IsNightShift = dto.IsNightShift;
                entity.IsWeekendShift = dto.IsWeekendShift;
                entity.IsHolidayShift = dto.IsHolidayShift;
                entity.AllowancePercentage = dto.AllowancePercentage;
                entity.LastModifiedBy = dto.LastModifiedBy;
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.RecordStatus = (int)dto.RecordStatus;

                await _context.SaveChangesAsync();

                return dto;
            }

            public async Task<bool> DeleteAsync(long id)
            {
                var entity = await _context.Shifts.FindAsync(id);
                if (entity == null) return false;

                _context.Shifts.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    
}
