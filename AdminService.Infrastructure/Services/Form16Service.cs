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

namespace AdminService.Infrastructure.Services
{
    public class Form16Service : IForm16Service
    {
        private readonly PayrollDbContext _context;

        public Form16Service(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeductionsComplianceDto.Form16Dto>> GetAllAsync()
        {
            return await _context.Form16s
                .Select(f => new DeductionsComplianceDto.Form16Dto
                {
                    FormId = f.FormId,
                    EmployeeId = f.EmployeeId,
                    FinancialYear = f.FinancialYear,
                    FilePath = f.FilePath,
                    GeneratedAt = f.GeneratedAt,
                    RecordStatus = f.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<DeductionsComplianceDto.Form16Dto?> GetByIdAsync(long formId)
        {
            var entity = await _context.Form16s.FindAsync(formId);
            if (entity == null) return null;

            return new DeductionsComplianceDto.Form16Dto
            {
                FormId = entity.FormId,
                EmployeeId = entity.EmployeeId,
                FinancialYear = entity.FinancialYear,
                FilePath = entity.FilePath,
                GeneratedAt = entity.GeneratedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<DeductionsComplianceDto.Form16Dto> CreateAsync(DeductionsComplianceDto.CreateForm16Dto dto)
        {
            var entity = new Form16
            {
                EmployeeId = dto.EmployeeId,
                FinancialYear = dto.FinancialYear,
                FilePath = dto.FilePath,
                GeneratedAt = DateTime.UtcNow,
                CreatedBy = 1, // TODO: replace with logged-in user id
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.Form16s.Add(entity);
            await _context.SaveChangesAsync();

            return new DeductionsComplianceDto.Form16Dto
            {
                FormId = entity.FormId,
                EmployeeId = entity.EmployeeId,
                FinancialYear = entity.FinancialYear,
                FilePath = entity.FilePath,
                GeneratedAt = entity.GeneratedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<DeductionsComplianceDto.Form16Dto?> UpdateAsync(long formId, DeductionsComplianceDto.UpdateForm16Dto dto)
        {
            var entity = await _context.Form16s.FindAsync(formId);
            if (entity == null) return null;

            entity.EmployeeId = dto.EmployeeId;
            entity.FinancialYear = dto.FinancialYear;
            entity.FilePath = dto.FilePath;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = 1; // TODO: replace with logged-in user id
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new DeductionsComplianceDto.Form16Dto
            {
                FormId = entity.FormId,
                EmployeeId = entity.EmployeeId,
                FinancialYear = entity.FinancialYear,
                FilePath = entity.FilePath,
                GeneratedAt = entity.GeneratedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long formId)
        {
            var entity = await _context.Form16s.FindAsync(formId);
            if (entity == null) return false;

            _context.Form16s.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
