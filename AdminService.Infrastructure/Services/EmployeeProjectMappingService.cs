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
    public class EmployeeProjectMappingService : IEmployeeProjectMappingService
    {
        private readonly PayrollDbContext _context;

        public EmployeeProjectMappingService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeProjectMappingDto>> GetAllAsync()
        {
            return await _context.EmployeeProjectMappings
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Select(x => new EmployeeProjectMappingDto
                {
                    MappingId = x.MappingId,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.LastName, 
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.ProjectName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsCurrent = x.IsCurrent,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    RecordStatus = x.RecordStatus
                }).ToListAsync();
        }

        public async Task<EmployeeProjectMappingDto?> GetByIdAsync(long id)
        {
            var entity = await _context.EmployeeProjectMappings
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.MappingId == id);

            if (entity == null) return null;

            return new EmployeeProjectMappingDto
            {
                MappingId = entity.MappingId,
                EmployeeId = entity.EmployeeId,
                EmployeeName = entity.Employee.LastName,
                ProjectId = entity.ProjectId,
                ProjectName = entity.Project.ProjectName,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                IsCurrent = entity.IsCurrent,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<EmployeeProjectMappingDto> CreateAsync(EmployeeProjectMappingDto dto)
        {
            var entity = new EmployeeProjectMapping
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrent = dto.IsCurrent,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.EmployeeProjectMappings.Add(entity);
            await _context.SaveChangesAsync();


            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Project).LoadAsync();

            dto.MappingId = entity.MappingId;
            dto.CreatedOn = entity.CreatedOn;
            dto.EmployeeName = entity.Employee.LastName; 
            dto.ProjectName = entity.Project.ProjectName; 

            return dto;
        }

        public async Task<EmployeeProjectMappingDto?> UpdateAsync(long id, EmployeeProjectMappingDto dto)
        {
            var entity = await _context.EmployeeProjectMappings
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .FirstOrDefaultAsync(x => x.MappingId == id);

            if (entity == null) return null;


            if (!await _context.Employees.AnyAsync(e => e.EmployeeId == dto.EmployeeId))
                throw new InvalidOperationException($"EmployeeId {dto.EmployeeId} does not exist.");

            if (!await _context.Projects.AnyAsync(p => p.ProjectId == dto.ProjectId))
                throw new InvalidOperationException($"ProjectId {dto.ProjectId} does not exist.");


            entity.EmployeeId = dto.EmployeeId;
            entity.ProjectId = dto.ProjectId;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.IsCurrent = dto.IsCurrent;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Project).LoadAsync();

            dto.EmployeeName = entity.Employee.LastName; 
            dto.ProjectName = entity.Project.ProjectName; 

            return dto;
        }


        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.EmployeeProjectMappings.FindAsync(id);
            if (entity == null) return false;

            _context.EmployeeProjectMappings.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
