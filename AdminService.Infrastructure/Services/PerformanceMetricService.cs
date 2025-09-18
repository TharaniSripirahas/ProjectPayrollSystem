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
    public class PerformanceMetricService : IPerformanceMetricService
    {
        private readonly PayrollDbContext _context;

        public PerformanceMetricService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<List<PerformanceMetricDto>> GetAllAsync()
        {
            return await _context.PerformanceMetrics
                .Select(x => new PerformanceMetricDto
                {
                    MetricId = x.MetricId,
                    MetricName = x.MetricName,
                    Description = x.Description,
                    Unit = x.Unit,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    RecordStatus = x.RecordStatus
                }).ToListAsync();
        }

        public async Task<PerformanceMetricDto> GetByIdAsync(long id)
        {
            var entity = await _context.PerformanceMetrics.FindAsync(id);
            if (entity == null) return null;

            return new PerformanceMetricDto
            {
                MetricId = entity.MetricId,
                MetricName = entity.MetricName,
                Description = entity.Description,
                Unit = entity.Unit,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<PerformanceMetricDto> CreateAsync(PerformanceMetricDto dto)
        {
            var entity = new PerformanceMetric
            {
                MetricName = dto.MetricName,
                Description = dto.Description,
                Unit = dto.Unit,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = dto.RecordStatus
            };

            _context.PerformanceMetrics.Add(entity);
            await _context.SaveChangesAsync();

            dto.MetricId = entity.MetricId;
            dto.CreatedOn = entity.CreatedOn;

            return dto;
        }

        public async Task<PerformanceMetricDto> UpdateAsync(long id, PerformanceMetricDto dto)
        {
            var entity = await _context.PerformanceMetrics.FindAsync(id);
            if (entity == null) return null;

            entity.MetricName = dto.MetricName;
            entity.Description = dto.Description;
            entity.Unit = dto.Unit;
            entity.IsActive = dto.IsActive;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.PerformanceMetrics.FindAsync(id);
            if (entity == null) return false;

            _context.PerformanceMetrics.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
