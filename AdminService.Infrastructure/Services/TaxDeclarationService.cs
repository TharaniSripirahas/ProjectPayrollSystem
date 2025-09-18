using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Select(t => new DeductionsComplianceDto.TaxDeclarationDto
                {
                    DeclarationId = t.DeclarationId,
                    EmployeeId = t.EmployeeId,
                    FinancialYear = t.FinancialYear,
                    DeclaredAmount = t.DeclaredAmount,
                    VerifiedAmount = t.VerifiedAmount,
                    Status = t.Status,
                    SubmittedAt = t.SubmittedAt,
                    VerifiedBy = t.VerifiedBy,
                    VerifiedAt = t.VerifiedAt,
                    RecordStatus = t.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<DeductionsComplianceDto.TaxDeclarationDto?> GetByIdAsync(long declarationId)
        {
            var entity = await _context.TaxDeclarations.FindAsync(declarationId);
            if (entity == null) return null;

            return new DeductionsComplianceDto.TaxDeclarationDto
            {
                DeclarationId = entity.DeclarationId,
                EmployeeId = entity.EmployeeId,
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
                VerifiedAmount = 0,  // default
                Status = 0,          // default pending
                CreatedBy = 1,       // TODO: replace with logged-in user
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.TaxDeclarations.Add(entity);
            await _context.SaveChangesAsync();

            return new DeductionsComplianceDto.TaxDeclarationDto
            {
                DeclarationId = entity.DeclarationId,
                EmployeeId = entity.EmployeeId,
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
            entity.LastModifiedBy = 1; // TODO: replace with logged-in user
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new DeductionsComplianceDto.TaxDeclarationDto
            {
                DeclarationId = entity.DeclarationId,
                EmployeeId = entity.EmployeeId,
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
