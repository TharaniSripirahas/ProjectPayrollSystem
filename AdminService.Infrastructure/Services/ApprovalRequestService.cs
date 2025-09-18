using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Infrastructure.Services
{
    public class ApprovalRequestService : IApprovalRequestService
    {
        private readonly PayrollDbContext _context;

        public ApprovalRequestService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalRequestDto>> GetAllAsync()
        {
            return await _context.ApprovalRequests
                .Include(r => r.Workflow)
                .Include(r => r.CurrentLevel)
                .Select(r => new ApprovalRequestDto
                {
                    RequestId = r.RequestId,
                    WorkflowId = r.WorkflowId,
                    WorkflowName = r.Workflow != null ? r.Workflow.WorkflowName : null,
                    CurrentLevelId = r.CurrentLevelId,
                    CurrentLevelName = r.CurrentLevel != null ? $"Level {r.CurrentLevel.LevelNumber}" : null,
                    EntityTable = r.EntityTable,
                    RequesterId = r.RequesterId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    CreatedBy = r.CreatedBy,
                    CreatedOn = r.CreatedOn,
                    LastModifiedBy = r.LastModifiedBy,
                    LastModifiedOn = r.LastModifiedOn,
                    RecordStatus = r.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<ApprovalRequestDto?> GetByIdAsync(long requestId)
        {
            var entity = await _context.ApprovalRequests
                .Include(r => r.Workflow)
                .Include(r => r.CurrentLevel)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (entity == null) return null;

            return new ApprovalRequestDto
            {
                RequestId = entity.RequestId,
                WorkflowId = entity.WorkflowId,
                WorkflowName = entity.Workflow?.WorkflowName,
                CurrentLevelId = entity.CurrentLevelId,
                CurrentLevelName = entity.CurrentLevel != null ? $"Level {entity.CurrentLevel.LevelNumber}" : null,
                EntityTable = entity.EntityTable,
                RequesterId = entity.RequesterId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<ApprovalRequestDto> CreateAsync(CreateApprovalRequestDto dto)
        {
            var entity = new ApprovalRequest
            {
                WorkflowId = dto.WorkflowId,
                CurrentLevelId = dto.CurrentLevelId,
                EntityTable = dto.EntityTable,
                RequesterId = dto.RequesterId,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn ?? DateTime.UtcNow,
                LastModifiedBy = dto.LastModifiedBy,
                LastModifiedOn = dto.LastModifiedOn,
                RecordStatus = dto.RecordStatus,
                CreatedAt = DateTime.UtcNow
            };

            _context.ApprovalRequests.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.RequestId);
        }

        public async Task<ApprovalRequestDto?> UpdateAsync(long requestId, UpdateApprovalRequestDto dto)
        {
            var entity = await _context.ApprovalRequests.FindAsync(requestId);
            if (entity == null) return null;

            entity.CurrentLevelId = dto.CurrentLevelId;
            entity.EntityTable = dto.EntityTable;
            entity.RequesterId = dto.RequesterId;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = dto.LastModifiedOn ?? DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(requestId);
        }

        public async Task<bool> DeleteAsync(long requestId)
        {
            var entity = await _context.ApprovalRequests.FindAsync(requestId);
            if (entity == null) return false;

            _context.ApprovalRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
