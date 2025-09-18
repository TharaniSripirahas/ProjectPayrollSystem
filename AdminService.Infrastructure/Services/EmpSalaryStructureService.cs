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
    public class EmpSalaryStructureService : IEmpSalaryStructureService
    {
        private readonly PayrollDbContext _context;

        public EmpSalaryStructureService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmpSalaryStructureDto>> GetAllAsync()
        {
            return await _context.EmpSalaryStructures
                .Select(s => new EmpSalaryStructureDto
                {
                    StructureId = s.StructureId,
                    EmployeeName = s.EmployeeName,
                    TemplateId = s.TemplateId,
                    BasicSalary = s.BasicSalary,
                    EffectiveFrom = s.EffectiveFrom,
                    EffectiveTo = s.EffectiveTo,
                    IsCurrent = s.IsCurrent,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn,
                    RecordStatus = s.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<EmpSalaryStructureDto?> GetByIdAsync(long id)
        {
            var s = await _context.EmpSalaryStructures.FindAsync(id);
            if (s == null) return null;

            return new EmpSalaryStructureDto
            {
                StructureId = s.StructureId,
                EmployeeName = s.EmployeeName,
                TemplateId = s.TemplateId,
                BasicSalary = s.BasicSalary,
                EffectiveFrom = s.EffectiveFrom,
                EffectiveTo = s.EffectiveTo,
                IsCurrent = s.IsCurrent,
                CreatedBy = s.CreatedBy,
                CreatedOn = s.CreatedOn,
                LastModifiedBy = s.LastModifiedBy,
                LastModifiedOn = s.LastModifiedOn,
                RecordStatus = s.RecordStatus
            };
        }

        public async Task<EmpSalaryStructureDto> CreateAsync(EmpSalaryStructureDto dto)
        {
            var entity = new EmpSalaryStructure
            {
                EmployeeName = dto.EmployeeName,
                TemplateId = dto.TemplateId,
                BasicSalary = dto.BasicSalary,
                EffectiveFrom = dto.EffectiveFrom,
                EffectiveTo = dto.EffectiveTo,
                IsCurrent = dto.IsCurrent,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.EmpSalaryStructures.Add(entity);
            await _context.SaveChangesAsync();

            dto.StructureId = entity.StructureId;
            dto.CreatedOn = entity.CreatedOn;
            return dto;
        }

        public async Task<EmpSalaryStructureDto?> UpdateAsync(long id, EmpSalaryStructureDto dto)
        {
            var entity = await _context.EmpSalaryStructures.FindAsync(id);
            if (entity == null) return null;

            entity.EmployeeName = dto.EmployeeName;
            entity.TemplateId = dto.TemplateId;
            entity.BasicSalary = dto.BasicSalary;
            entity.EffectiveFrom = dto.EffectiveFrom;
            entity.EffectiveTo = dto.EffectiveTo;
            entity.IsCurrent = dto.IsCurrent;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.EmpSalaryStructures.Update(entity);
            await _context.SaveChangesAsync();

            dto.StructureId = entity.StructureId;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedOn = entity.LastModifiedOn;
            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.EmpSalaryStructures.FindAsync(id);
            if (entity == null) return false;

            _context.EmpSalaryStructures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
