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
                    PayrollRecordsCount = p.PayrollRecords.Count,
                    ReimbursementClaimsCount = p.ReimbursementClaims.Count,
                    PayslipNotificationsCount = p.PayslipNotifications.Count
                })
                .ToListAsync();
        }

        public async Task<PayrollCycleDto?> GetByIdAsync(long payrollCycleId)
        {
            var cycle = await _context.PayrollCycles
                .Include(p => p.PayrollRecords)
                .Include(p => p.ReimbursementClaims)
                .Include(p => p.PayslipNotifications)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PayrollCycleId == payrollCycleId);

            if (cycle == null) return null;

            return new PayrollCycleDto
            {
                PayrollCycleId = cycle.PayrollCycleId,
                PayrollCycleName = cycle.PayrollCycleName,
                StartDate = cycle.StartDate,
                EndDate = cycle.EndDate,
                PaymentDate = cycle.PaymentDate,
                ProcessedAt = cycle.ProcessedAt,
                PayrollRecordsCount = cycle.PayrollRecords.Count,
                ReimbursementClaimsCount = cycle.ReimbursementClaims.Count,
                PayslipNotificationsCount = cycle.PayslipNotifications.Count
            };
        }

        public async Task<PayrollCycleDto> CreateAsync(CreatePayrollCycleDto dto)
        {
            var cycle = new PayrollCycle
            {
                PayrollCycleName = dto.PayrollCycleName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PaymentDate = dto.PaymentDate,
                CreatedBy = 1, // TODO: replace with logged-in user
                CreatedOn = DateTime.UtcNow,
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
            cycle.LastModifiedBy = 1; // TODO: replace with logged-in user
            cycle.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload related counts
            await _context.Entry(cycle).Collection(c => c.PayrollRecords).LoadAsync();
            await _context.Entry(cycle).Collection(c => c.ReimbursementClaims).LoadAsync();
            await _context.Entry(cycle).Collection(c => c.PayslipNotifications).LoadAsync();

            return new PayrollCycleDto
            {
                PayrollCycleId = cycle.PayrollCycleId,
                PayrollCycleName = cycle.PayrollCycleName,
                StartDate = cycle.StartDate,
                EndDate = cycle.EndDate,
                PaymentDate = cycle.PaymentDate,
                ProcessedAt = cycle.ProcessedAt,
                PayrollRecordsCount = cycle.PayrollRecords.Count,
                ReimbursementClaimsCount = cycle.ReimbursementClaims.Count,
                PayslipNotificationsCount = cycle.PayslipNotifications.Count
            };
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
