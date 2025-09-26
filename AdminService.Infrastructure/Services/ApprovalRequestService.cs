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
                    RequesterName = r.Requester != null
                    ? $"{r.Requester.FirstName} {r.Requester.LastName}"
                    : null,
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
                .Include(r => r.Requester)
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
                RequesterName = entity.Requester != null
                ? $"{entity.Requester.FirstName} {entity.Requester.LastName}"
                : null,
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

            var createdRequest = await _context.ApprovalRequests
                .Include(r => r.Workflow)
                .Include(r => r.CurrentLevel)
                .Include(r => r.Requester)
                .FirstOrDefaultAsync(r => r.RequestId == entity.RequestId);

            if (createdRequest == null)
                throw new Exception("ApprovalRequest not found after creation.");

            return new ApprovalRequestDto
            {
                RequestId = createdRequest.RequestId,
                WorkflowId = createdRequest.WorkflowId,
                WorkflowName = createdRequest.Workflow?.WorkflowName,
                CurrentLevelId = createdRequest.CurrentLevelId,
                CurrentLevelName = createdRequest.CurrentLevel != null
                    ? $"Level {createdRequest.CurrentLevel.LevelNumber}"
                    : null,  
                EntityTable = createdRequest.EntityTable,
                RequesterId = createdRequest.RequesterId,
                RequesterName = createdRequest.Requester != null
                    ? $"{createdRequest.Requester.FirstName} {createdRequest.Requester.LastName}"
                   : null,
                CreatedAt = createdRequest.CreatedAt,
                UpdatedAt = createdRequest.UpdatedAt,
                CreatedBy = createdRequest.CreatedBy,
                CreatedOn = createdRequest.CreatedOn,
                LastModifiedBy = createdRequest.LastModifiedBy,
                LastModifiedOn = createdRequest.LastModifiedOn,
                RecordStatus = createdRequest.RecordStatus
            };
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