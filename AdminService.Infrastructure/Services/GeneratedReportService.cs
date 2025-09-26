using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.PayslipsReportingDto;

namespace AdminService.Infrastructure.Services
{
    public class GeneratedReportService : IGeneratedReportService
    {
        private readonly PayrollDbContext _context;

        public GeneratedReportService(PayrollDbContext context)
        {
            _context = context;
        }

        // Get all reports
        public async Task<IEnumerable<GeneratedReportDto>> GetAllAsync()
        {
            return await _context.GeneratedReports
                .Include(r => r.GeneratedByNavigation)
                .Select(r => new GeneratedReportDto
                {
                    ReportId = r.ReportId,
                    FilePath = r.FilePath,
                    ReportPeriod = r.ReportPeriod,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    GeneratedBy = r.GeneratedBy,
                    GeneratedByName = r.GeneratedByNavigation != null
                        ? r.GeneratedByNavigation.FirstName + " " + r.GeneratedByNavigation.LastName
                        : null,
                    GeneratedAt = r.GeneratedAt,
                    RecordStatus = r.RecordStatus
                })
                .ToListAsync();
        }

        // Get report by ID
        public async Task<GeneratedReportDto?> GetByIdAsync(long reportId)
        {
            var entity = await _context.GeneratedReports
                .Include(r => r.GeneratedByNavigation)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);

            if (entity == null) return null;

            return new GeneratedReportDto
            {
                ReportId = entity.ReportId,
                FilePath = entity.FilePath,
                ReportPeriod = entity.ReportPeriod,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                GeneratedBy = entity.GeneratedBy,
                GeneratedByName = entity.GeneratedByNavigation != null
                    ? entity.GeneratedByNavigation.FirstName + " " + entity.GeneratedByNavigation.LastName
                    : null,
                GeneratedAt = entity.GeneratedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        // Create new report
        public async Task<GeneratedReportDto> CreateAsync(CreateGeneratedReportDto dto, long loggedInUserId = 1)
        {
            var employee = await _context.Employees.FindAsync(dto.GeneratedBy);
            if (employee == null)
                throw new Exception($"Employee with ID {dto.GeneratedBy} not found.");

            var entity = new GeneratedReport
            {
                FilePath = dto.FilePath,
                ReportPeriod = dto.ReportPeriod,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                GeneratedBy = dto.GeneratedBy,
                GeneratedAt = dto.GeneratedAt ?? DateTime.UtcNow,
                CreatedBy = loggedInUserId,  
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1,
                GeneratedByNavigation = employee
            };

            _context.GeneratedReports.Add(entity);
            await _context.SaveChangesAsync();

            return new GeneratedReportDto
            {
                ReportId = entity.ReportId,
                FilePath = entity.FilePath,
                ReportPeriod = entity.ReportPeriod,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                GeneratedBy = entity.GeneratedBy,
                GeneratedByName = employee.FirstName + " " + employee.LastName,
                GeneratedAt = entity.GeneratedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        // update report
        public async Task<GeneratedReportDto?> UpdateAsync(long reportId, UpdateGeneratedReportDto dto, long loggedInUserId = 1)
        {
            var entity = await _context.GeneratedReports
                .Include(r => r.GeneratedByNavigation)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);

            if (entity == null) return null;

            if (entity.GeneratedBy != dto.GeneratedBy)
            {
                var employee = await _context.Employees.FindAsync(dto.GeneratedBy);
                if (employee == null)
                    throw new Exception($"Employee with ID {dto.GeneratedBy} not found.");

                entity.GeneratedBy = dto.GeneratedBy;
                entity.GeneratedByNavigation = employee;
            }

            entity.FilePath = dto.FilePath;
            entity.ReportPeriod = dto.ReportPeriod;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = loggedInUserId; 
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new GeneratedReportDto
            {
                ReportId = entity.ReportId,
                FilePath = entity.FilePath,
                ReportPeriod = entity.ReportPeriod,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                GeneratedBy = entity.GeneratedBy,
                GeneratedByName = entity.GeneratedByNavigation != null
                    ? entity.GeneratedByNavigation.FirstName + " " + entity.GeneratedByNavigation.LastName
                    : null,
                GeneratedAt = entity.GeneratedAt,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long reportId)
        {
            var entity = await _context.GeneratedReports.FindAsync(reportId);
            if (entity == null) return false;

            _context.GeneratedReports.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
