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
                .Select(tc => new TemplateComponentDto
                {
                    TemplateComponentId = tc.TemplateComponentId,
                    TemplateId = tc.TemplateId,
                    ComponentId = tc.ComponentId,
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
            var tc = await _context.TemplateComponents.FindAsync(id);
            if (tc == null) return null;

            return new TemplateComponentDto
            {
                TemplateComponentId = tc.TemplateComponentId,
                TemplateId = tc.TemplateId,
                ComponentId = tc.ComponentId,
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

            dto.TemplateComponentId = entity.TemplateComponentId;
            dto.CreatedOn = entity.CreatedOn;
            return dto;
        }

        public async Task<TemplateComponentDto?> UpdateAsync(long id, TemplateComponentDto dto)
        {
            var entity = await _context.TemplateComponents.FindAsync(id);
            if (entity == null) return null;

            entity.TemplateId = dto.TemplateId;
            entity.ComponentId = dto.ComponentId;
            entity.CalculationType = dto.CalculationType;
            entity.Value = dto.Value;
            entity.MaxLimit = dto.MaxLimit;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.TemplateComponents.Update(entity);
            await _context.SaveChangesAsync();

            dto.TemplateComponentId = entity.TemplateComponentId;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedOn = entity.LastModifiedOn;
            return dto;
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
