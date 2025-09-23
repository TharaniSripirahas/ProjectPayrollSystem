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
    public class SalaryTemplateService : ISalaryTemplateService
    {
        private readonly PayrollDbContext _context;

        public SalaryTemplateService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaryTemplateDto>> GetAllAsync()
        {
            return await _context.SalaryTemplates
                .Select(t => new SalaryTemplateDto
                {
                    TemplateId = t.TemplateId,
                    TemplateName = t.TemplateName,
                    Description = t.Description,
                    EmployeeTypeName = t.EmployeeType.TypeName,
                    CreatedBy = t.CreatedBy,
                    CreatedOn = t.CreatedOn,
                    LastModifiedBy = t.LastModifiedBy,
                    LastModifiedOn = t.LastModifiedOn,
                    RecordStatus = t.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<SalaryTemplateDto?> GetByIdAsync(long id)
        {
            var t = await _context.SalaryTemplates
             .Include(t => t.EmployeeType)
             .FirstOrDefaultAsync(x => x.TemplateId == id);

            if (t == null) return null;

            return new SalaryTemplateDto
            {
                TemplateId = t.TemplateId,
                TemplateName = t.TemplateName,
                EmployeeTypeId = t.EmployeeTypeId,
                EmployeeTypeName = t.EmployeeType.TypeName,
                Description = t.Description,
                CreatedBy = t.CreatedBy,
                CreatedOn = t.CreatedOn,
                LastModifiedBy = t.LastModifiedBy,
                LastModifiedOn = t.LastModifiedOn,
                RecordStatus = t.RecordStatus
            };

        }

        public async Task<SalaryTemplateDto> CreateAsync(SalaryTemplateDto dto)
        {
            var employeeTypeExists = await _context.EmployeeTypes.AnyAsync(e => e.EmployeeTypeId == dto.EmployeeTypeId);
            if (!employeeTypeExists)
                throw new Exception($"EmployeeTypeId {dto.EmployeeTypeId} does not exist.");

            var entity = new SalaryTemplate
            {
                TemplateName = dto.TemplateName,
                EmployeeTypeId = dto.EmployeeTypeId,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.SalaryTemplates.Add(entity);
            await _context.SaveChangesAsync();

            dto.TemplateId = entity.TemplateId;
            dto.CreatedOn = entity.CreatedOn;

            return dto;
        }



        public async Task<SalaryTemplateDto?> UpdateAsync(long id, SalaryTemplateDto dto)
        {
            var entity = await _context.SalaryTemplates.FindAsync(id);
            if (entity == null) return null;

            entity.TemplateName = dto.TemplateName;
            entity.EmployeeTypeId = dto.EmployeeTypeId;
            entity.Description = dto.Description;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.SalaryTemplates.Update(entity);
            await _context.SaveChangesAsync();

            dto.TemplateId = entity.TemplateId;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedOn = entity.LastModifiedOn;

            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.SalaryTemplates.FindAsync(id);
            if (entity == null) return false;

            _context.SalaryTemplates.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
