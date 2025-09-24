using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Infrastructure.Services
{
    public class PayrollCycleService : IPayrollCycleService
    {
        private readonly PayrollDbContext _context;

        public PayrollCycleService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayrollCycleDto>> GetAllAsync()
        {
            return await _context.PayrollCycles
                .AsNoTracking()
                .Select(p => new PayrollCycleDto
                {
                    PayrollCycleId = p.PayrollCycleId,
                    PayrollCycleName = p.PayrollCycleName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PaymentDate = p.PaymentDate,
                    ProcessedAt = p.ProcessedAt,
                    PayrollRecordsCount = _context.PayrollRecords.Count(r => r.PayrollCycleId == p.PayrollCycleId),
                    ReimbursementClaimsCount = _context.ReimbursementClaims.Count(r => r.PayrollCycleId == p.PayrollCycleId),
                    PayslipNotificationsCount = _context.PayslipNotifications.Count(n => n.PayrollCycleId == p.PayrollCycleId)
                })
                .ToListAsync();
        }

        public async Task<PayrollCycleDto?> GetByIdAsync(long payrollCycleId)
        {
            var cycle = await _context.PayrollCycles
                .AsNoTracking()
                .Where(p => p.PayrollCycleId == payrollCycleId)
                .Select(p => new PayrollCycleDto
                {
                    PayrollCycleId = p.PayrollCycleId,
                    PayrollCycleName = p.PayrollCycleName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PaymentDate = p.PaymentDate,
                    ProcessedAt = p.ProcessedAt,
                    PayrollRecordsCount = _context.PayrollRecords.Count(r => r.PayrollCycleId == p.PayrollCycleId),
                    ReimbursementClaimsCount = _context.ReimbursementClaims.Count(r => r.PayrollCycleId == p.PayrollCycleId),
                    PayslipNotificationsCount = _context.PayslipNotifications.Count(n => n.PayrollCycleId == p.PayrollCycleId)
                })
                .FirstOrDefaultAsync();

            return cycle;
        }

        public async Task<PayrollCycleDto> CreateAsync(CreatePayrollCycleDto dto)
        {
            var cycle = new PayrollCycle
            {
                PayrollCycleName = dto.PayrollCycleName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PaymentDate = dto.PaymentDate,
                CreatedBy = 1, 
                CreatedOn = DateTime.UtcNow,
                ProcessedAt = dto.ProcessedAt,
                RecordStatus = 1
            };

            _context.PayrollCycles.Add(cycle);
            await _context.SaveChangesAsync();

            return new PayrollCycleDto
            {
                PayrollCycleId = cycle.PayrollCycleId,
                PayrollCycleName = cycle.PayrollCycleName,
                StartDate = cycle.StartDate,
                EndDate = cycle.EndDate,
                PaymentDate = cycle.PaymentDate,
                ProcessedAt = cycle.ProcessedAt,
                PayrollRecordsCount = 0,
                ReimbursementClaimsCount = 0,
                PayslipNotificationsCount = 0
            };
        }

        public async Task<PayrollCycleDto?> UpdateAsync(long payrollCycleId, UpdatePayrollCycleDto dto)
        {
            var cycle = await _context.PayrollCycles.FindAsync(payrollCycleId);
            if (cycle == null) return null;

            cycle.PayrollCycleName = dto.PayrollCycleName;
            cycle.StartDate = dto.StartDate;
            cycle.EndDate = dto.EndDate;
            cycle.PaymentDate = dto.PaymentDate;
            cycle.LastModifiedBy = 1; 
            cycle.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await _context.PayrollCycles
                .AsNoTracking()
                .Where(p => p.PayrollCycleId == payrollCycleId)
                .Select(p => new PayrollCycleDto
                {
                    PayrollCycleId = p.PayrollCycleId,
                    PayrollCycleName = p.PayrollCycleName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PaymentDate = p.PaymentDate,
                    ProcessedAt = p.ProcessedAt,
                    PayrollRecordsCount = _context.PayrollRecords.Count(r => r.PayrollCycleId == p.PayrollCycleId),
                    ReimbursementClaimsCount = _context.ReimbursementClaims.Count(r => r.PayrollCycleId == p.PayrollCycleId),
                    PayslipNotificationsCount = _context.PayslipNotifications.Count(n => n.PayrollCycleId == p.PayrollCycleId)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(long payrollCycleId)
        {
            var cycle = await _context.PayrollCycles.FindAsync(payrollCycleId);
            if (cycle == null) return false;

            _context.PayrollCycles.Remove(cycle);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
