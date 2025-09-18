using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.PayslipsReportingDto;

namespace AdminService.Infrastructure.Services
{
    public class PayslipAccessLogService : IPayslipAccessLogService
    {
        private readonly PayrollDbContext _context;

        public PayslipAccessLogService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayslipAccessLogDto>> GetAllAsync()
        {
            return await _context.PayslipAccessLogs
                .Include(l => l.Payslip)
                .Include(l => l.AccessedByNavigation)
                .Select(l => new PayslipAccessLogDto
                {
                    LogId = l.LogId,
                    PayslipId = l.PayslipId,
                    AccessedBy = l.AccessedBy,
                    AccessedByName = l.AccessedByNavigation != null ? l.AccessedByNavigation.FirstName + " " + l.AccessedByNavigation.LastName : null,
                    AccessTime = l.AccessTime,
                    IpAddress = l.IpAddress,
                    RecordStatus = l.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<PayslipAccessLogDto?> GetByIdAsync(long logId)
        {
            var entity = await _context.PayslipAccessLogs
                .Include(l => l.Payslip)
                .Include(l => l.AccessedByNavigation)
                .FirstOrDefaultAsync(l => l.LogId == logId);

            if (entity == null) return null;

            return new PayslipAccessLogDto
            {
                LogId = entity.LogId,
                PayslipId = entity.PayslipId,
                AccessedBy = entity.AccessedBy,
                AccessedByName = entity.AccessedByNavigation != null ? entity.AccessedByNavigation.FirstName + " " + entity.AccessedByNavigation.LastName : null,
                AccessTime = entity.AccessTime,
                IpAddress = entity.IpAddress,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<PayslipAccessLogDto> CreateAsync(CreatePayslipAccessLogDto dto)
        {
            // Validate Payslip
            var payslip = await _context.PayslipStorages.FindAsync(dto.PayslipId);
            if (payslip == null)
                throw new Exception($"Payslip with ID {dto.PayslipId} not found.");

            // Validate Employee
            var employee = await _context.Employees.FindAsync(dto.AccessedBy);
            if (employee == null)
                throw new Exception($"Employee with ID {dto.AccessedBy} not found.");

            var entity = new PayslipAccessLog
            {
                PayslipId = dto.PayslipId,
                AccessedBy = dto.AccessedBy,
                AccessTime = dto.AccessTime ?? DateTime.UtcNow,
                IpAddress = dto.IpAddress,
                CreatedBy = 1, 
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1,
                Payslip = payslip,
                AccessedByNavigation = employee
            };

            _context.PayslipAccessLogs.Add(entity);
            await _context.SaveChangesAsync();

            return new PayslipAccessLogDto
            {
                LogId = entity.LogId,
                PayslipId = entity.PayslipId,
                AccessedBy = entity.AccessedBy,
                AccessedByName = employee.FirstName + " " + employee.LastName,
                AccessTime = entity.AccessTime,
                IpAddress = entity.IpAddress,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<PayslipAccessLogDto?> UpdateAsync(long logId, UpdatePayslipAccessLogDto dto)
        {
            var entity = await _context.PayslipAccessLogs
                .Include(l => l.Payslip)
                .Include(l => l.AccessedByNavigation)
                .FirstOrDefaultAsync(l => l.LogId == logId);

            if (entity == null) return null;

            // Update foreign keys if changed
            if (entity.PayslipId != dto.PayslipId)
            {
                var payslip = await _context.PayslipStorages.FindAsync(dto.PayslipId);
                if (payslip == null)
                    throw new Exception($"Payslip with ID {dto.PayslipId} not found.");
                entity.PayslipId = dto.PayslipId;
                entity.Payslip = payslip;
            }

            if (entity.AccessedBy != dto.AccessedBy)
            {
                var employee = await _context.Employees.FindAsync(dto.AccessedBy);
                if (employee == null)
                    throw new Exception($"Employee with ID {dto.AccessedBy} not found.");
                entity.AccessedBy = dto.AccessedBy;
                entity.AccessedByNavigation = employee;
            }

            entity.AccessTime = dto.AccessTime;
            entity.IpAddress = dto.IpAddress;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = 1; // TODO: replace with logged-in user
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new PayslipAccessLogDto
            {
                LogId = entity.LogId,
                PayslipId = entity.PayslipId,
                AccessedBy = entity.AccessedBy,
                AccessedByName = entity.AccessedByNavigation != null ? entity.AccessedByNavigation.FirstName + " " + entity.AccessedByNavigation.LastName : null,
                AccessTime = entity.AccessTime,
                IpAddress = entity.IpAddress,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long logId)
        {
            var entity = await _context.PayslipAccessLogs.FindAsync(logId);
            if (entity == null) return false;

            _context.PayslipAccessLogs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
