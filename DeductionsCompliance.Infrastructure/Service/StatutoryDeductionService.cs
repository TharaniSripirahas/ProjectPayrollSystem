using DeductionsCompliance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.DeductionsComplianceDto;

namespace DeductionsCompliance.Infrastructure.Service
{
    public class StatutoryDeductionService : IStatutoryDeductionService
    {
        private readonly PayrollDbContext _context;

        public StatutoryDeductionService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatutoryDeductionDto>> GetAllAsync()
        {
            return await _context.StatutoryDeductions
                .AsNoTracking()
                .Select(d => new StatutoryDeductionDto
                {
                    DeductionId = d.DeductionId,
                    DeductionName = d.DeductionName,
                    DeductionCode = d.DeductionCode,
                    CalculationMethod = d.CalculationMethod,
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedOn = d.LastModifiedOn,
                    RecordStatus = d.RecordStatus,
                    EmployeeStatutoryDetailsCount = d.EmployeeStatutoryDetails.Count
                })
                .ToListAsync();
        }

        public async Task<StatutoryDeductionDto?> GetByIdAsync(long deductionId)
        {
            var d = await _context.StatutoryDeductions
                .Include(x => x.EmployeeStatutoryDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DeductionId == deductionId);

            if (d == null) return null;

            return new StatutoryDeductionDto
            {
                DeductionId = d.DeductionId,
                DeductionName = d.DeductionName,
                DeductionCode = d.DeductionCode,
                CalculationMethod = d.CalculationMethod,
                CreatedBy = d.CreatedBy,
                CreatedOn = d.CreatedOn,
                LastModifiedBy = d.LastModifiedBy,
                LastModifiedOn = d.LastModifiedOn,
                RecordStatus = d.RecordStatus,
                EmployeeStatutoryDetailsCount = d.EmployeeStatutoryDetails.Count
            };
        }

        public async Task<StatutoryDeductionDto> CreateAsync(CreateStatutoryDeductionDto dto)
        {
            var entity = new StatutoryDeduction
            {
                DeductionName = dto.DeductionName,
                DeductionCode = dto.DeductionCode,
                CalculationMethod = dto.CalculationMethod,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn ?? DateTime.UtcNow,
                LastModifiedBy = dto.LastModifiedBy,
                LastModifiedOn = dto.LastModifiedOn,
                RecordStatus = dto.RecordStatus
            };

            _context.StatutoryDeductions.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.DeductionId) ?? throw new Exception("Error retrieving created deduction.");
        }


        public async Task<StatutoryDeductionDto?> UpdateAsync(long deductionId, UpdateStatutoryDeductionDto dto)
        {
            var entity = await _context.StatutoryDeductions.FindAsync(deductionId);
            if (entity == null) return null;

            entity.DeductionName = dto.DeductionName;
            entity.DeductionCode = dto.DeductionCode;
            entity.CalculationMethod = dto.CalculationMethod;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = 1; 
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.DeductionId);
        }

        public async Task<bool> DeleteAsync(long deductionId)
        {
            var entity = await _context.StatutoryDeductions.FindAsync(deductionId);
            if (entity == null) return false;

            _context.StatutoryDeductions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
