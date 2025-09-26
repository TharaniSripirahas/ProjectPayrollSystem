using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public class TaxDeclarationService : ITaxDeclarationService
    {
        private readonly PayrollDbContext _context;

        public TaxDeclarationService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeductionsComplianceDto.TaxDeclarationDto>> GetAllAsync()
        {
            return await _context.TaxDeclarations
                .Include(t => t.Employee)
                .Select(t => new DeductionsComplianceDto.TaxDeclarationDto
                {
                    DeclarationId = t.DeclarationId,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee.FirstName + " " + t.Employee.LastName,
                    FinancialYear = t.FinancialYear,
                    DeclaredAmount = t.DeclaredAmount,
                    VerifiedAmount = t.VerifiedAmount,
                    Status = t.Status,
                    SubmittedAt = t.SubmittedAt,
                    VerifiedBy = t.VerifiedBy,
                    VerifiedAt = t.VerifiedAt,
                    RecordStatus = t.RecordStatus
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DeductionsComplianceDto.TaxDeclarationDto?> GetByIdAsync(long declarationId)
        {
            var entity = await _context.TaxDeclarations
                .Include(t => t.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.DeclarationId == declarationId);

            if (entity == null) return null;

            return new DeductionsComplianceDto.TaxDeclarationDto
            {
                DeclarationId = entity.DeclarationId,
                EmployeeId = entity.EmployeeId,
                EmployeeName = entity.Employee.FirstName + " " + entity.Employee.LastName,
                FinancialYear = entity.FinancialYear,
                DeclaredAmount = entity.DeclaredAmount,
                VerifiedAmount = entity.VerifiedAmount,
                Status = entity.Status,
                SubmittedAt = entity.SubmittedAt,
                VerifiedBy = entity.VerifiedBy,
                VerifiedAt = entity.VerifiedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<DeductionsComplianceDto.TaxDeclarationDto> CreateAsync(DeductionsComplianceDto.CreateTaxDeclarationDto dto)
        {
            var entity = new TaxDeclaration
            {
                EmployeeId = dto.EmployeeId,
                FinancialYear = dto.FinancialYear,
                DeclaredAmount = dto.DeclaredAmount,
                VerifiedAmount = 0,  
                Status = 0,         
                CreatedBy = dto.CreatedBy > 0 ? dto.CreatedBy : 1,
                CreatedOn = dto.CreatedOn != default ? dto.CreatedOn : DateTime.UtcNow,
                RecordStatus = dto.RecordStatus > 0 ? dto.RecordStatus : 1
            };

            _context.TaxDeclarations.Add(entity);
            await _context.SaveChangesAsync();

            var saved = await _context.TaxDeclarations
                .Include(t => t.Employee)
                .FirstAsync(t => t.DeclarationId == entity.DeclarationId);

            return new DeductionsComplianceDto.TaxDeclarationDto
            {
                DeclarationId = saved.DeclarationId,
                EmployeeId = saved.EmployeeId,
                EmployeeName = saved.Employee.FirstName + " " + saved.Employee.LastName,
                FinancialYear = saved.FinancialYear,
                DeclaredAmount = saved.DeclaredAmount,
                VerifiedAmount = saved.VerifiedAmount,
                Status = saved.Status,
                SubmittedAt = saved.SubmittedAt,
                VerifiedBy = saved.VerifiedBy,
                VerifiedAt = saved.VerifiedAt,
                RecordStatus = saved.RecordStatus
            };
        }

        public async Task<DeductionsComplianceDto.TaxDeclarationDto?> UpdateAsync(long declarationId, DeductionsComplianceDto.UpdateTaxDeclarationDto dto)
        {
            var entity = await _context.TaxDeclarations.FindAsync(declarationId);
            if (entity == null) return null;

            entity.EmployeeId = dto.EmployeeId;
            entity.FinancialYear = dto.FinancialYear;
            entity.DeclaredAmount = dto.DeclaredAmount;
            entity.VerifiedAmount = dto.VerifiedAmount;
            entity.Status = dto.Status;
            entity.SubmittedAt = dto.SubmittedAt;
            entity.VerifiedBy = dto.VerifiedBy;
            entity.VerifiedAt = dto.VerifiedAt;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = dto.LastModifiedBy ?? 1;
            entity.LastModifiedOn = dto.LastModifiedOn ?? DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updated = await _context.TaxDeclarations
                .Include(t => t.Employee)
                .FirstAsync(t => t.DeclarationId == entity.DeclarationId);

            return new DeductionsComplianceDto.TaxDeclarationDto
            {
                DeclarationId = updated.DeclarationId,
                EmployeeId = updated.EmployeeId,
                EmployeeName = updated.Employee.FirstName + " " + updated.Employee.LastName,
                FinancialYear = updated.FinancialYear,
                DeclaredAmount = updated.DeclaredAmount,
                VerifiedAmount = updated.VerifiedAmount,
                Status = updated.Status,
                SubmittedAt = updated.SubmittedAt,
                VerifiedBy = updated.VerifiedBy,
                VerifiedAt = updated.VerifiedAt,
                RecordStatus = updated.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long declarationId)
        {
            var entity = await _context.TaxDeclarations.FindAsync(declarationId);
            if (entity == null) return false;

            _context.TaxDeclarations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
