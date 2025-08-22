using EmployeeService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.DTOs;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Services
{
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private readonly DbContextPayrollProject _context;

        public EmployeeSkillService(DbContextPayrollProject context)
        {
            _context = context;
        }

        public async Task<List<EmployeeSkillDto>> GetAllAsync()
        {
            return await _context.EmployeeSkills
                .Select(es => new EmployeeSkillDto
                {
                    EmployeeSkillId = es.EmployeeSkillId,
                    EmployeeId = es.EmployeeId,
                    SkillId = es.SkillId,
                    ProficiencyLevel = es.ProficiencyLevel,
                    Certificate = es.Certificate,
                    CreatedBy = es.CreatedBy,
                    CreatedOn = es.CreatedOn,
                    LastModifiedBy = es.LastModifiedBy,
                    LastModifiedOn = es.LastModifiedOn,
                    RecordStatus = es.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<EmployeeSkillDto?> GetByIdAsync(long id)
        {
            var es = await _context.EmployeeSkills.FindAsync(id);
            if (es == null) return null;

            return new EmployeeSkillDto
            {
                EmployeeSkillId = es.EmployeeSkillId,
                EmployeeId = es.EmployeeId,
                SkillId = es.SkillId,
                ProficiencyLevel = es.ProficiencyLevel,
                Certificate = es.Certificate,
                CreatedBy = es.CreatedBy,
                CreatedOn = es.CreatedOn,
                LastModifiedBy = es.LastModifiedBy,
                LastModifiedOn = es.LastModifiedOn,
                RecordStatus = es.RecordStatus
            };
        }

        public async Task<EmployeeSkillDto> CreateAsync(EmployeeSkillDto dto)
        {
            try
            {
                var entity = new EmployeeSkill
                {
                    EmployeeId = dto.EmployeeId,
                    SkillId = dto.SkillId,
                    ProficiencyLevel = dto.ProficiencyLevel,
                    Certificate = dto.Certificate,
                    CreatedBy = dto.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    RecordStatus = dto.RecordStatus,
                    LastModifiedBy = dto.CreatedBy,
                    LastModifiedOn = DateTime.UtcNow
                };

                _context.EmployeeSkills.Add(entity);
                await _context.SaveChangesAsync();

                return new EmployeeSkillDto
                {
                    EmployeeSkillId = entity.EmployeeSkillId,
                    EmployeeId = entity.EmployeeId,
                    SkillId = entity.SkillId,
                    ProficiencyLevel = entity.ProficiencyLevel,
                    Certificate = entity.Certificate,
                    CreatedBy = entity.CreatedBy,
                    CreatedOn = entity.CreatedOn,
                    LastModifiedBy = entity.LastModifiedBy,
                    LastModifiedOn = entity.LastModifiedOn,
                    RecordStatus = entity.RecordStatus
                };
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
        }

        public async Task<bool> UpdateAsync(long id, EmployeeSkillDto dto)
        {
            var entity = await _context.EmployeeSkills.FindAsync(id);
            if (entity == null) return false;

            entity.EmployeeId = dto.EmployeeId;
            entity.SkillId = dto.SkillId;
            entity.ProficiencyLevel = dto.ProficiencyLevel;
            entity.Certificate = dto.Certificate;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.EmployeeSkills.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.EmployeeSkills.FindAsync(id);
            if (entity == null) return false;

            _context.EmployeeSkills.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
