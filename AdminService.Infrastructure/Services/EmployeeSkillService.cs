using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private readonly PayrollDbContext _context;

        public EmployeeSkillService(PayrollDbContext context)
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
                    EmployeeName = es.Employee.FirstName + " " + es.Employee.LastName,
                    SkillId = es.SkillId,
                    SkillName = es.Skill.SkillName,
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
            var es = await _context.EmployeeSkills
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(e => e.EmployeeSkillId == id);

            if (es == null) return null;

            return new EmployeeSkillDto
            {
                EmployeeSkillId = es.EmployeeSkillId,
                EmployeeId = es.EmployeeId,
                EmployeeName = es.Employee.FirstName + " " + es.Employee.LastName,
                SkillId = es.SkillId,
                SkillName = es.Skill.SkillName,
                ProficiencyLevel = es.ProficiencyLevel,
                Certificate = es.Certificate,
                CreatedBy = es.CreatedBy,
                CreatedOn = es.CreatedOn,
                LastModifiedBy = es.LastModifiedBy,
                LastModifiedOn = es.LastModifiedOn,
                RecordStatus = es.RecordStatus
            };
        }


        public async Task<EmployeeSkillDto> CreateAsync(EmployeeSkillCreateDto dto)
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

            var savedEntity = await _context.EmployeeSkills
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(e => e.EmployeeSkillId == entity.EmployeeSkillId);

            return new EmployeeSkillDto
            {
                EmployeeSkillId = savedEntity.EmployeeSkillId,
                EmployeeId = savedEntity.EmployeeId,
                EmployeeName = savedEntity.Employee.FirstName + " " + savedEntity.Employee.LastName,
                SkillId = savedEntity.SkillId,
                SkillName = savedEntity.Skill.SkillName,
                ProficiencyLevel = savedEntity.ProficiencyLevel,
                Certificate = savedEntity.Certificate,
                CreatedBy = savedEntity.CreatedBy,
                CreatedOn = savedEntity.CreatedOn,
                LastModifiedBy = savedEntity.LastModifiedBy,
                LastModifiedOn = savedEntity.LastModifiedOn,
                RecordStatus = savedEntity.RecordStatus
            };
        }

        public async Task<EmployeeSkillDto?> UpdateAsync(long id, EmployeeSkillDto dto)
        {
            var entity = await _context.EmployeeSkills
                .Include(es => es.Employee)
                .Include(es => es.Skill)
                .FirstOrDefaultAsync(es => es.EmployeeSkillId == id);

            if (entity == null) return null;

            // Update properties
            entity.EmployeeId = dto.EmployeeId;
            entity.SkillId = dto.SkillId;
            entity.ProficiencyLevel = dto.ProficiencyLevel;
            entity.Certificate = dto.Certificate;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            _context.EmployeeSkills.Update(entity);
            await _context.SaveChangesAsync();

            // Return with EmployeeName + SkillName
            return new EmployeeSkillDto
            {
                EmployeeSkillId = entity.EmployeeSkillId,
                EmployeeId = entity.EmployeeId,
                EmployeeName = entity.Employee?.FirstName + " " + entity.Employee?.LastName, // ✅ mapped
                SkillId = entity.SkillId,
                SkillName = entity.Skill?.SkillName, // ✅ mapped
                ProficiencyLevel = entity.ProficiencyLevel,
                Certificate = entity.Certificate,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
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
