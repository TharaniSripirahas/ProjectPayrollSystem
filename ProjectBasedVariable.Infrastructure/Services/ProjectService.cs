using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.NonEntities;
using ProjectBasedVariable.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBasedVariable.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PayrollDbContext _context;

        public ProjectService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            return await _context.Projects
                .Select(p => new ProjectDto
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName,
                    ClientName = p.ClientName,
                    ManagerName = p.Manager.LastName,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsActive = p.IsActive,
                    ManagerId = p.ManagerId,
                    RecordStatus = p.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<ProjectDto?> GetByIdAsync(long projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Manager) // include manager
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project == null) return null;

            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                IsActive = project.IsActive,
                ManagerId = project.ManagerId,
                ManagerName = project.Manager?.LastName, 
                RecordStatus = project.RecordStatus
            };
        }


        public async Task<ProjectDto> CreateAsync(ProjectDto dto)
        {
            var project = new Payroll.Common.Models.Project
            {
                ProjectName = dto.ProjectName,
                ClientName = dto.ClientName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive,
                ManagerId = dto.ManagerId,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            await _context.Entry(project).Reference(p => p.Manager).LoadAsync();

            dto.ProjectId = project.ProjectId;
            dto.ManagerName = project.Manager?.LastName ;

            return dto;
        }


        public async Task<ProjectDto?> UpdateAsync(long projectId, ProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null) return null;

            project.ProjectName = dto.ProjectName;
            project.ClientName = dto.ClientName;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.IsActive = dto.IsActive;
            project.ManagerId = dto.ManagerId;
            project.LastModifiedBy = 1; 
            project.LastModifiedOn = DateTime.UtcNow;
            project.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(long projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
