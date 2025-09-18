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
                .Select(x => new ProjectPerformanceDto
                {
                    PerformanceId = x.PerformanceId,
                    EmployeeId = x.EmployeeId,
                    ProjectId = x.ProjectId,
                    MetricId = x.MetricId,
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

        public async Task<ProjectPerformanceDto> GetByIdAsync(long id)
        {
            var entity = await _context.ProjectPerformances.FindAsync(id);
            if (entity == null) return null;

            return new ProjectPerformanceDto
            {
                PerformanceId = entity.PerformanceId,
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId,
                MetricId = entity.MetricId,
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

            dto.PerformanceId = entity.PerformanceId;
            dto.CreatedOn = entity.CreatedOn;
            return dto;
        }

        public async Task<ProjectPerformanceDto> UpdateAsync(long id, ProjectPerformanceDto dto)
        {
            var entity = await _context.ProjectPerformances.FindAsync(id);
            if (entity == null) return null;

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
