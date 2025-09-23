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
    public class EmpSalaryStructureService : IEmpSalaryStructureService
    {
        private readonly PayrollDbContext _context;

        public EmpSalaryStructureService(PayrollDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<IEnumerable<EmpSalaryStructureDto>> GetAllAsync()
        {
            return await _context.EmpSalaryStructures
                .Include(e => e.Employee)
                .Include(e => e.Template)
                .Select(s => new EmpSalaryStructureDto
                {
                    StructureId = s.StructureId,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.LastName, 
                    TemplateId = s.TemplateId,
                    TemplateName = s.Template.TemplateName,
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

        // GET BY ID
        public async Task<EmpSalaryStructureDto?> GetByIdAsync(long id)
        {
            var s = await _context.EmpSalaryStructures
                .Include(e => e.Employee)
                .Include(e => e.Template)
                .FirstOrDefaultAsync(x => x.StructureId == id);

            if (s == null) return null;

            return new EmpSalaryStructureDto
            {
                StructureId = s.StructureId,
                EmployeeId = s.EmployeeId,
                EmployeeName = s.Employee.LastName,
                TemplateId = s.TemplateId,
                TemplateName = s.Template.TemplateName,
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

        // CREATE
        public async Task<EmpSalaryStructureDto> CreateAsync(EmpSalaryStructureDto dto)
        {
            var entity = new EmpSalaryStructure
            {
                EmployeeId = dto.EmployeeId,
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

            // Fetch names for the DTO
            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Template).LoadAsync();

            dto.StructureId = entity.StructureId;
            dto.EmployeeName = entity.Employee.LastName;
            dto.TemplateName = entity.Template.TemplateName;
            dto.CreatedOn = entity.CreatedOn;
            return dto;
        }

        // UPDATE
        public async Task<EmpSalaryStructureDto?> UpdateAsync(long id, EmpSalaryStructureDto dto)
        {
            var entity = await _context.EmpSalaryStructures.FindAsync(id);
            if (entity == null) return null;

            entity.EmployeeId = dto.EmployeeId;
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

            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Template).LoadAsync();

            dto.StructureId = entity.StructureId;
            dto.EmployeeName = entity.Employee.LastName;
            dto.TemplateName = entity.Template.TemplateName;
            dto.CreatedOn = entity.CreatedOn;
            dto.LastModifiedOn = entity.LastModifiedOn;

            return dto;
        }

        // DELETE
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
