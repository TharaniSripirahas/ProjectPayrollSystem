using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.NonEntities;
using SalaryComponent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryComponent.Infrastructure.Services
{
    public class SalaryComponentService : ISalaryComponentService
    {
        private readonly PayrollDbContext _context;

        public SalaryComponentService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaryComponentDto>> GetAllAsync()
        {
            return await _context.SalaryComponents
                .Select(c => new SalaryComponentDto
                {
                    ComponentId = c.ComponentId,
                    ComponentName = c.ComponentName,
                    ComponentType = c.ComponentType,
                    IsTaxable = c.IsTaxable,
                    IsStatutory = c.IsStatutory,
                    Description = c.Description,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    LastModifiedBy = c.LastModifiedBy,
                    LastModifiedOn = c.LastModifiedOn,
                    RecordStatus = c.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<SalaryComponentDto?> GetByIdAsync(long id)
        {
            var c = await _context.SalaryComponents.FindAsync(id);
            if (c == null) return null;

            return new SalaryComponentDto
            {
                ComponentId = c.ComponentId,
                ComponentName = c.ComponentName,
                ComponentType = c.ComponentType,
                IsTaxable = c.IsTaxable,
                IsStatutory = c.IsStatutory,
                Description = c.Description,
                CreatedBy = c.CreatedBy,
                CreatedOn = c.CreatedOn,
                LastModifiedBy = c.LastModifiedBy,
                LastModifiedOn = c.LastModifiedOn,
                RecordStatus = c.RecordStatus
            };
        }

        public async Task<SalaryComponentDto> CreateAsync(SalaryComponentDto dto)
        {
            var entity = new Payroll.Common.Models.SalaryComponent
            {
                ComponentName = dto.ComponentName,
                ComponentType = dto.ComponentType,
                IsTaxable = dto.IsTaxable,
                IsStatutory = dto.IsStatutory,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.SalaryComponents.Add(entity);
            await _context.SaveChangesAsync();

            dto.ComponentId = entity.ComponentId;
            dto.CreatedOn = entity.CreatedOn;

            return dto;
        }

        public async Task<SalaryComponentDto?> UpdateAsync(long id, SalaryComponentDto dto)
        {
            var entity = await _context.SalaryComponents.FindAsync(id);
            if (entity == null) return null;

            entity.ComponentName = dto.ComponentName;
            entity.ComponentType = dto.ComponentType;
            entity.IsTaxable = dto.IsTaxable;
            entity.IsStatutory = dto.IsStatutory;
            entity.Description = dto.Description;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.SalaryComponents.Update(entity);
            await _context.SaveChangesAsync();

            dto.ComponentId = entity.ComponentId;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedOn = entity.LastModifiedOn;

            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.SalaryComponents.FindAsync(id);
            if (entity == null) return false;

            _context.SalaryComponents.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
