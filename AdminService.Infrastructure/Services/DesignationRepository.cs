using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class DesignationRepository : IDesignationService
    {
        private readonly PayrollDbContext _context;

        public DesignationRepository(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<List<DesignationDto>> GetAllAsync()
        {
            return await _context.Designations
                .Include(d => d.Department) 
                .Select(d => new DesignationDto
                {
                    DesignationId = d.DesignationId,
                    DesignationName = d.DesignationName,
                    Description = d.Description,
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department.DepartmentName, 
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    LastModifiedBy = d.LastModifiedBy,
                    LastModifiedOn = d.LastModifiedOn,
                    RecordStatus = d.RecordStatus
                })
                .ToListAsync();
        }


        public async Task<DesignationDto?> GetByIdAsync(long id)
        {
            var entity = await _context.Designations
                .Include(d => d.Department)
                .FirstOrDefaultAsync(x => x.DesignationId == id);

            if (entity == null) return null;

            return new DesignationDto
            {
                DesignationId = entity.DesignationId,
                DesignationName = entity.DesignationName,
                Description = entity.Description,
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.Department.DepartmentName,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<DesignationDto> CreateAsync(DesignationDto dto)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == dto.DepartmentId);
            if (department == null)
                throw new ArgumentException($"Department with ID {dto.DepartmentId} does not exist.");

            var entity = new Designation
            {
                DesignationName = dto.DesignationName,
                Description = dto.Description,
                DepartmentId = dto.DepartmentId,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus,
                LastModifiedBy = dto.CreatedBy,
                LastModifiedOn = DateTime.UtcNow
            };

            _context.Designations.Add(entity);
            await _context.SaveChangesAsync();

            return new DesignationDto
            {
                DesignationId = entity.DesignationId,
                DesignationName = entity.DesignationName,
                Description = entity.Description,
                DepartmentId = entity.DepartmentId,
                DepartmentName = department.DepartmentName, 
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }




        public async Task<bool> UpdateAsync(long id, DesignationDto dto)
        {
            var entity = await _context.Designations.FindAsync(id);
            if (entity == null) return false;

            entity.DesignationName = dto.DesignationName;
            entity.Description = dto.Description;
            entity.DepartmentId = dto.DepartmentId;
            entity.RecordStatus = dto.RecordStatus;

            entity.LastModifiedBy = dto.LastModifiedBy ?? dto.CreatedBy;
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.Designations.FindAsync(id);
            if (entity == null) return false;

            _context.Designations.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
