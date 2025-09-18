using AdminService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
                .Select(a => new ApprovalActionDto
                {
                    ActionId = a.ActionId,
                    RequestId = a.RequestId,
                    LevelId = a.LevelId,
                    ApproverId = a.ApproverId,
                    ActionType = a.ActionType,
                    Comments = a.Comments,
                    ActionDate = a.ActionDate
                })
                .ToListAsync();
        }

        public async Task<ApprovalActionDto?> GetByIdAsync(long actionId)
        {
            var entity = await _context.ApprovalActions.FindAsync(actionId);
            if (entity == null) return null;

            return new ApprovalActionDto
            {
                ActionId = entity.ActionId,
                RequestId = entity.RequestId,
                LevelId = entity.LevelId,
                ApproverId = entity.ApproverId,
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

            return new ApprovalActionDto
            {
                ActionId = entity.ActionId,
                RequestId = entity.RequestId,
                LevelId = entity.LevelId,
                ApproverId = entity.ApproverId,
                ActionType = entity.ActionType,
                Comments = entity.Comments,
                ActionDate = entity.ActionDate
            };
        }

        public async Task<ApprovalActionDto?> UpdateAsync(long actionId, UpdateApprovalActionDto dto)
        {
            var entity = await _context.ApprovalActions.FindAsync(actionId);
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
                LevelId = entity.LevelId,
                ApproverId = entity.ApproverId,
                ActionType = entity.ActionType,
                Comments = entity.Comments,
                ActionDate = entity.ActionDate
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

