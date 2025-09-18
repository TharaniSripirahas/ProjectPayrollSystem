using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Infrastructure.Services
{
    public class ApprovalWorkflowService : IApprovalWorkflowService
    {
        private readonly PayrollDbContext _context;

        public ApprovalWorkflowService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalWorkflowDto>> GetAllAsync()
        {
            return await _context.ApprovalWorkflows
                .Select(x => new ApprovalWorkflowDto
                {
                    WorkflowId = x.WorkflowId,
                    WorkflowName = x.WorkflowName,
                    EntityType = x.EntityType,
                    IsActive = x.IsActive == 1,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ApprovalWorkflowDto?> GetByIdAsync(long workflowId)
        {
            var entity = await _context.ApprovalWorkflows.FindAsync(workflowId);
            if (entity == null) return null;

            return new ApprovalWorkflowDto
            {
                WorkflowId = entity.WorkflowId,
                WorkflowName = entity.WorkflowName,
                EntityType = entity.EntityType,
                IsActive = entity.IsActive == 1,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<ApprovalWorkflowDto> CreateAsync(CreateApprovalWorkflowDto dto)
        {
            var entity = new ApprovalWorkflow
            {
                WorkflowName = dto.WorkflowName,
                EntityType = dto.EntityType,
                IsActive = dto.IsActive,
                CreatedAt = dto.CreatedAt ?? DateTime.UtcNow,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn != default ? dto.CreatedOn : DateTime.UtcNow,
                LastModifiedBy = dto.LastModifiedBy,
                LastModifiedOn = dto.LastModifiedOn,
                RecordStatus = dto.RecordStatus
            };

            _context.ApprovalWorkflows.Add(entity);
            await _context.SaveChangesAsync();

            return new ApprovalWorkflowDto
            {
                WorkflowId = entity.WorkflowId,
                WorkflowName = entity.WorkflowName,
                EntityType = entity.EntityType,
                IsActive = entity.IsActive == 1,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<ApprovalWorkflowDto?> UpdateAsync(long workflowId, UpdateApprovalWorkflowDto dto)
        {
            var entity = await _context.ApprovalWorkflows.FindAsync(workflowId);
            if (entity == null) return null;

            entity.WorkflowName = dto.WorkflowName;
            entity.EntityType = dto.EntityType;
            entity.IsActive = dto.IsActive ? 1 : 0;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();

            return new ApprovalWorkflowDto
            {
                WorkflowId = entity.WorkflowId,
                WorkflowName = entity.WorkflowName,
                EntityType = entity.EntityType,
                IsActive = entity.IsActive == 1,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(long workflowId)
        {
            var entity = await _context.ApprovalWorkflows.FindAsync(workflowId);
            if (entity == null) return false;

            _context.ApprovalWorkflows.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
