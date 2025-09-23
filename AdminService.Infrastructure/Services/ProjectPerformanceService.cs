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
    public class ProjectPerformanceService : IProjectPerformanceService
    {
        private readonly PayrollDbContext _context;

        public ProjectPerformanceService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectPerformanceDto>> GetAllAsync()
        {
            return await _context.ProjectPerformances
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Metric)
                .Select(x => new ProjectPerformanceDto
                {
                    PerformanceId = x.PerformanceId,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.LastName,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.ProjectName,
                    MetricId = x.MetricId,
                    MetricName = x.Metric.MetricName,
                    PeriodStart = x.PeriodStart,
                    PeriodEnd = x.PeriodEnd,
                    AchievedValue = x.AchievedValue,
                    TargetValue = x.TargetValue,
                    BonusAmount = x.BonusAmount,
                    ApprovedBy = x.ApprovedBy,
                    ApprovedAt = x.ApprovedAt,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    RecordStatus = x.RecordStatus
                }).ToListAsync();
        }

        public async Task<ProjectPerformanceDto?> GetByIdAsync(long id)
        {
            var entity = await _context.ProjectPerformances
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Metric)
                .FirstOrDefaultAsync(x => x.PerformanceId == id);

            if (entity == null) return null;

            return new ProjectPerformanceDto
            {
                PerformanceId = entity.PerformanceId,
                EmployeeId = entity.EmployeeId,
                EmployeeName = entity.Employee.LastName,
                ProjectId = entity.ProjectId,
                ProjectName = entity.Project.ProjectName,
                MetricId = entity.MetricId,
                MetricName = entity.Metric.MetricName,
                PeriodStart = entity.PeriodStart,
                PeriodEnd = entity.PeriodEnd,
                AchievedValue = entity.AchievedValue,
                TargetValue = entity.TargetValue,
                BonusAmount = entity.BonusAmount,
                ApprovedBy = entity.ApprovedBy,
                ApprovedAt = entity.ApprovedAt,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<ProjectPerformanceDto> CreateAsync(ProjectPerformanceDto dto)
        {
            var entity = new ProjectPerformance
            {
                EmployeeId = dto.EmployeeId,
                ProjectId = dto.ProjectId,
                MetricId = dto.MetricId,
                PeriodStart = dto.PeriodStart,
                PeriodEnd = dto.PeriodEnd,
                AchievedValue = dto.AchievedValue,
                TargetValue = dto.TargetValue,
                BonusAmount = dto.BonusAmount,
                ApprovedBy = dto.ApprovedBy,
                ApprovedAt = dto.ApprovedAt,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.ProjectPerformances.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Project).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Metric).LoadAsync();

            dto.PerformanceId = entity.PerformanceId;
            dto.CreatedOn = entity.CreatedOn;
            dto.EmployeeName = entity.Employee.LastName;
            dto.ProjectName = entity.Project.ProjectName;
            dto.MetricName = entity.Metric.MetricName;

            return dto;
        }

        public async Task<ProjectPerformanceDto?> UpdateAsync(long id, ProjectPerformanceDto dto)
        {
            var entity = await _context.ProjectPerformances
                .Include(x => x.Employee)
                .Include(x => x.Project)
                .Include(x => x.Metric)
                .FirstOrDefaultAsync(x => x.PerformanceId == id);

            if (entity == null) return null;

            if (!await _context.Employees.AnyAsync(e => e.EmployeeId == dto.EmployeeId))
                throw new InvalidOperationException($"EmployeeId {dto.EmployeeId} does not exist.");

            if (!await _context.Projects.AnyAsync(p => p.ProjectId == dto.ProjectId))
                throw new InvalidOperationException($"ProjectId {dto.ProjectId} does not exist.");

            if (!await _context.PerformanceMetrics.AnyAsync(m => m.MetricId == dto.MetricId))
                throw new InvalidOperationException($"MetricId {dto.MetricId} does not exist.");

            entity.EmployeeId = dto.EmployeeId;
            entity.ProjectId = dto.ProjectId;
            entity.MetricId = dto.MetricId;
            entity.PeriodStart = dto.PeriodStart;
            entity.PeriodEnd = dto.PeriodEnd;
            entity.AchievedValue = dto.AchievedValue;
            entity.TargetValue = dto.TargetValue;
            entity.BonusAmount = dto.BonusAmount;
            entity.ApprovedBy = dto.ApprovedBy;
            entity.ApprovedAt = dto.ApprovedAt;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(e => e.Employee).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Project).LoadAsync();
            await _context.Entry(entity).Reference(e => e.Metric).LoadAsync();

            dto.EmployeeName = entity.Employee.LastName;
            dto.ProjectName = entity.Project.ProjectName;
            dto.MetricName = entity.Metric.MetricName;

            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.ProjectPerformances.FindAsync(id);
            if (entity == null) return false;

            _context.ProjectPerformances.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}