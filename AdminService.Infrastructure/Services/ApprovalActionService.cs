using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Infrastructure.Services
{
    public class ApprovalActionService : IApprovalActionService
    {
        private readonly PayrollDbContext _context;

        public ApprovalActionService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApprovalActionDto>> GetAllAsync()
        {
            return await _context.ApprovalActions
                .Include(a => a.Request)  
                .Include(a => a.Approver)  
                .Select(a => new ApprovalActionDto
                {
                    ActionId = a.ActionId,
                    RequestId = a.RequestId,
                    RequestName = a.Request.EntityTable,
                    LevelId = a.LevelId,
                    ApproverId = a.ApproverId,
                    ApproverName = a.Approver != null ? a.Approver.LastName : null,
                    ActionType = a.ActionType,
                    Comments = a.Comments,
                    ActionDate = a.ActionDate
                    
                })
                .ToListAsync();
        }

        public async Task<ApprovalActionDto?> GetByIdAsync(long actionId)
        {
            var entity = await _context.ApprovalActions
                .Include(a => a.Request)
                .Include(a => a.Approver)
                .FirstOrDefaultAsync(a => a.ActionId == actionId);

            if (entity == null) return null;

            return new ApprovalActionDto
            {
                ActionId = entity.ActionId,
                RequestId = entity.RequestId,
                RequestName = entity.Request.EntityTable,
                LevelId = entity.LevelId,
                ApproverId = entity.ApproverId,
                ApproverName = entity.Approver != null ? entity.Approver.LastName : null,
                ActionType = entity.ActionType,
                Comments = entity.Comments,
                ActionDate = entity.ActionDate
            };
        }

        public async Task<ApprovalActionDto> CreateAsync(CreateApprovalActionDto dto)
        {
            var entity = new ApprovalAction
            {
                RequestId = dto.RequestId,
                LevelId = dto.LevelId,
                ApproverId = dto.ApproverId,
                ActionType = dto.ActionType,
                Comments = dto.Comments,
                ActionDate = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 0
            };

            _context.ApprovalActions.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(a => a.Request).LoadAsync();
            await _context.Entry(entity).Reference(a => a.Approver).LoadAsync();

            return new ApprovalActionDto
            {
                ActionId = entity.ActionId,
                RequestId = entity.RequestId,
                RequestName = entity.Request.EntityTable,
                LevelId = entity.LevelId,
                ApproverId = entity.ApproverId,
                ApproverName = entity.Approver?.LastName,
                ActionType = entity.ActionType,
                Comments = entity.Comments,
                ActionDate = entity.ActionDate
            };
        }

        public async Task<ApprovalActionDto?> UpdateAsync(long actionId, UpdateApprovalActionDto dto)
        {
            var entity = await _context.ApprovalActions
                .Include(a => a.Request)
                .Include(a => a.Approver)
                .FirstOrDefaultAsync(a => a.ActionId == actionId);

            if (entity == null) return null;

            entity.ActionType = dto.ActionType;
            entity.Comments = dto.Comments;
            entity.LastModifiedBy = dto.LastModifiedBy;
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApprovalActionDto
            {
                ActionId = entity.ActionId,
                RequestId = entity.RequestId,
                RequestName = entity.Request.EntityTable,
                LevelId = entity.LevelId,
                ApproverId = entity.ApproverId,
                ApproverName = entity.Approver?.LastName,
                ActionType = entity.ActionType,
                Comments = entity.Comments,
                ActionDate = entity.ActionDate,
            };
        }

        public async Task<bool> DeleteAsync(long actionId)
        {
            var entity = await _context.ApprovalActions.FindAsync(actionId);
            if (entity == null) return false;

            _context.ApprovalActions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
