using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        private readonly DbContextPayrollProject _context;

        public SkillService(DbContextPayrollProject context)
        {
            _context = context;
        }

        public async Task<List<SkillDto>> GetAllAsync()
        {
            return await _context.Skills
                .Select(s => new SkillDto
                {
                    SkillId = s.SkillId,
                    SkillName = s.SkillName,
                    Category = s.Category,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn,
                    RecordStatus = s.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<SkillDto?> GetByIdAsync(long id)
        {
            var s = await _context.Skills.FindAsync(id);
            if (s == null) return null;

            return new SkillDto
            {
                SkillId = s.SkillId,
                SkillName = s.SkillName,
                Category = s.Category,
                CreatedBy = s.CreatedBy,
                CreatedOn = s.CreatedOn,
                LastModifiedBy = s.LastModifiedBy,
                LastModifiedOn = s.LastModifiedOn,
                RecordStatus = s.RecordStatus
            };
        }

        public async Task<SkillDto> CreateAsync(SkillDto dto)
        {
            var entity = new Skill
            {
                SkillName = dto.SkillName,
                Category = dto.Category,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus,
                LastModifiedBy = dto.LastModifiedBy,
                LastModifiedOn = DateTime.UtcNow
            };

            _context.Skills.Add(entity);
            await _context.SaveChangesAsync();

            return new SkillDto
            {
                SkillId = entity.SkillId,
                SkillName = entity.SkillName,
                Category = entity.Category,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                RecordStatus = entity.RecordStatus,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn
            };
        }

        public async Task<bool> UpdateAsync(long id, SkillDto dto)
        {
            var s = await _context.Skills.FindAsync(id);
            if (s == null) return false;

            s.SkillName = dto.SkillName;
            s.Category = dto.Category;
            s.LastModifiedBy = dto.LastModifiedBy;
            s.LastModifiedOn = DateTime.UtcNow;
            s.RecordStatus = dto.RecordStatus;

            _context.Skills.Update(s);
            return await _context.SaveChangesAsync() > 0;
        }


        
        public async Task<bool> DeleteAsync(long id)
        {
            var s = await _context.Skills.FindAsync(id);
            if (s == null) return false;

            _context.Skills.Remove(s);
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
