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
    public class TemplateComponentService : ITemplateComponentService
    {
        private readonly PayrollDbContext _context;

        public TemplateComponentService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TemplateComponentDto>> GetAllAsync()
        {
            return await _context.TemplateComponents
                .Include(tc => tc.Template)   
                .Include(tc => tc.Component)  
                .Select(tc => new TemplateComponentDto
                {
                    TemplateComponentId = tc.TemplateComponentId,
                    TemplateId = tc.TemplateId,
                    TemplateName = tc.Template.TemplateName,   
                    ComponentId = tc.ComponentId,
                    ComponentName = tc.Component.ComponentName, 
                    CalculationType = tc.CalculationType,
                    Value = tc.Value,
                    MaxLimit = tc.MaxLimit,
                    CreatedBy = tc.CreatedBy,
                    CreatedOn = tc.CreatedOn,
                    LastModifiedBy = tc.LastModifiedBy,
                    LastModifiedOn = tc.LastModifiedOn,
                    RecordStatus = tc.RecordStatus
                })
                .ToListAsync();
        }


        public async Task<TemplateComponentDto?> GetByIdAsync(long id)
        {
            var tc = await _context.TemplateComponents
                .Include(t => t.Template)
                .Include(c => c.Component)
                .FirstOrDefaultAsync(x => x.TemplateComponentId == id);

            if (tc == null) return null;

            return new TemplateComponentDto
            {
                TemplateComponentId = tc.TemplateComponentId,
                TemplateId = tc.TemplateId,
                TemplateName = tc.Template.TemplateName,
                ComponentId = tc.ComponentId,
                ComponentName = tc.Component.ComponentName,
                CalculationType = tc.CalculationType,
                Value = tc.Value,
                MaxLimit = tc.MaxLimit,
                CreatedBy = tc.CreatedBy,
                CreatedOn = tc.CreatedOn,
                LastModifiedBy = tc.LastModifiedBy,
                LastModifiedOn = tc.LastModifiedOn,
                RecordStatus = tc.RecordStatus
            };
        }


        public async Task<TemplateComponentDto> CreateAsync(TemplateComponentDto dto)
        {
            var entity = new TemplateComponent
            {
                TemplateId = dto.TemplateId,
                ComponentId = dto.ComponentId,
                CalculationType = dto.CalculationType,
                Value = dto.Value,
                MaxLimit = dto.MaxLimit,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.TemplateComponents.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Template).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Component).LoadAsync();

            return new TemplateComponentDto
            {
                TemplateComponentId = entity.TemplateComponentId,
                TemplateId = entity.TemplateId,
                TemplateName = entity.Template.TemplateName,        
                ComponentId = entity.ComponentId,
                ComponentName = entity.Component.ComponentName, 
                CalculationType = entity.CalculationType,
                Value = entity.Value,
                MaxLimit = entity.MaxLimit,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }


        public async Task<TemplateComponentDto?> UpdateAsync(long id, TemplateComponentDto dto)
        {
            var entity = await _context.TemplateComponents
                .Include(tc => tc.Template)
                .Include(tc => tc.Component)
                .FirstOrDefaultAsync(tc => tc.TemplateComponentId == id);

            if (entity == null) return null;

            var templateExists = await _context.SalaryTemplates
                .AnyAsync(t => t.TemplateId == dto.TemplateId);
            if (!templateExists)
                throw new InvalidOperationException($"TemplateId {dto.TemplateId} does not exist.");

            var componentExists = await _context.SalaryComponents
                .AnyAsync(c => c.ComponentId == dto.ComponentId);
            if (!componentExists)
                throw new InvalidOperationException($"ComponentId {dto.ComponentId} does not exist.");

            entity.TemplateId = dto.TemplateId;
            entity.ComponentId = dto.ComponentId;
            entity.CalculationType = dto.CalculationType;
            entity.Value = dto.Value;
            entity.MaxLimit = dto.MaxLimit;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Template).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Component).LoadAsync();

            return new TemplateComponentDto
            {
                TemplateComponentId = entity.TemplateComponentId,
                TemplateId = entity.TemplateId,
                TemplateName = entity.Template?.TemplateName,
                ComponentId = entity.ComponentId,
                ComponentName = entity.Component?.ComponentName,
                CalculationType = entity.CalculationType,
                Value = entity.Value,
                MaxLimit = entity.MaxLimit,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.TemplateComponents.FindAsync(id);
            if (entity == null) return false;

            _context.TemplateComponents.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
