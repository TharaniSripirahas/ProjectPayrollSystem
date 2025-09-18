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
    public class ApprovalLevelService : IApprovalLevelService
    {
        private readonly PayrollDbContext _context;

        public ApprovalLevelService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalLevelDto>> GetAllAsync()
        {
            return await _context.ApprovalLevels
                .Select(x => new ApprovalLevelDto
                {
                    LevelId = x.LevelId,
                    WorkflowId = x.WorkflowId,
                    LevelNumber = x.LevelNumber,
                    ApproverRoleId = x.ApproverRoleId,
                    WorkflowName = x.Workflow != null ? x.Workflow.WorkflowName : null, 
                    IsFinalApproval = x.IsFinalApproval == 1
                })
                .ToListAsync();
        }

        public async Task<ApprovalLevelDto?> GetByIdAsync(long levelId)
        {
            var entity = await _context.ApprovalLevels.FindAsync(levelId);
            if (entity == null) return null;

            return new ApprovalLevelDto
            {
                LevelId = entity.LevelId,
                WorkflowId = entity.WorkflowId,
                LevelNumber = entity.LevelNumber,
                ApproverRoleId = entity.ApproverRoleId,
                WorkflowName = entity.Workflow != null ? entity.Workflow.WorkflowName : null,
                IsFinalApproval = entity.IsFinalApproval == 1
            };
        }

        public async Task<ApprovalLevelDto> CreateAsync(CreateApprovalLevelDto dto)
        {
            var entity = new ApprovalLevel
            {
                WorkflowId = dto.WorkflowId,
                LevelNumber = dto.LevelNumber,
                ApproverRoleId = dto.ApproverRoleId,
                IsFinalApproval = dto.IsFinalApproval ? 1 : 0,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn != default ? dto.CreatedOn : DateTime.UtcNow,
                LastModifiedBy = dto.LastModifiedBy,
                LastModifiedOn = dto.LastModifiedOn,
                RecordStatus = dto.RecordStatus
            };

            _context.ApprovalLevels.Add(entity);
            await _context.SaveChangesAsync();

            return new ApprovalLevelDto
            {
                LevelId = entity.LevelId,
                WorkflowId = entity.WorkflowId,
                LevelNumber = entity.LevelNumber,
                ApproverRoleId = entity.ApproverRoleId,
                IsFinalApproval = entity.IsFinalApproval == 1,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<ApprovalLevelDto?> UpdateAsync(long levelId, UpdateApprovalLevelDto dto)
        {
            var entity = await _context.ApprovalLevels.FindAsync(levelId);
            if (entity == null) return null;

            entity.LevelNumber = dto.LevelNumber;
            entity.ApproverRoleId = dto.ApproverRoleId;
            entity.IsFinalApproval = dto.IsFinalApproval ? 1 : 0;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = dto.LastModifiedOn ?? DateTime.UtcNow;
            entity.RecordStatus = dto.RecordStatus;

            await _context.SaveChangesAsync();

            return new ApprovalLevelDto
            {
                LevelId = entity.LevelId,
                WorkflowId = entity.WorkflowId,
                LevelNumber = entity.LevelNumber,
                ApproverRoleId = entity.ApproverRoleId,
                IsFinalApproval = entity.IsFinalApproval == 1,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                LastModifiedBy = entity.LastModifiedBy,
                LastModifiedOn = entity.LastModifiedOn,
                RecordStatus = entity.RecordStatus
            };
        }


        public async Task<bool> DeleteAsync(long levelId)
        {
            var entity = await _context.ApprovalLevels.FindAsync(levelId);
            if (entity == null) return false;

            _context.ApprovalLevels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
