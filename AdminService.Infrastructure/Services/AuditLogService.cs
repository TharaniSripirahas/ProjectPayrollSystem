using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace AdminService.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly PayrollDbContext _context;

        public AuditLogService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .Select(a => new AuditLogDto
                {
                    LogId = a.LogId,
                    UserId = a.UserId,
                    Username = a.User.Username,
                    Action = a.Action,
                    TableName = a.TableName,
                    RecordId = a.RecordId,
                    OldValue = a.OldValue,
                    NewValue = a.NewValue,
                    IpAddress = a.IpAddress,
                    RecordStatus = a.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<AuditLogDto?> GetByIdAsync(long logId)
        {
            var a = await _context.AuditLogs.Include(a => a.User)
                .FirstOrDefaultAsync(x => x.LogId == logId);

            if (a == null) return null;

            return new AuditLogDto
            {
                LogId = a.LogId,
                UserId = a.UserId,
                Username = a.User.Username,
                Action = a.Action,
                TableName = a.TableName,
                RecordId = a.RecordId,
                OldValue = a.OldValue,
                NewValue = a.NewValue,
                IpAddress = a.IpAddress,
                RecordStatus = a.RecordStatus
            };
        }

        public async Task<AuditLogDto> CreateAsync(CreateAuditLogDto dto)
        {
            var entity = new AuditLog
            {
                UserId = dto.UserId,
                Action = dto.Action,
                TableName = dto.TableName,
                RecordId = dto.RecordId,
                OldValue = dto.OldValue,
                NewValue = dto.NewValue,
                IpAddress = dto.IpAddress,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.AuditLogs.Add(entity);
            await _context.SaveChangesAsync();

            // Fetch with related User
            var created = await _context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.LogId == entity.LogId)
                .Select(a => new AuditLogDto
                {
                    LogId = a.LogId,
                    UserId = a.UserId,
                    Username = a.User.Username,
                    Action = a.Action,
                    TableName = a.TableName,
                    RecordId = a.RecordId,
                    OldValue = a.OldValue,
                    NewValue = a.NewValue,
                    IpAddress = a.IpAddress,
                    RecordStatus = a.RecordStatus
                })
                .FirstAsync();

            return created;
        }

        public async Task<AuditLogDto?> UpdateAsync(long logId, UpdateAuditLogDto dto)
        {
            var entity = await _context.AuditLogs.FindAsync(logId);
            if (entity == null) return null;

            entity.UserId = dto.UserId;
            entity.Action = dto.Action;
            entity.TableName = dto.TableName;
            entity.RecordId = dto.RecordId;
            entity.OldValue = dto.OldValue;
            entity.NewValue = dto.NewValue;
            entity.IpAddress = dto.IpAddress;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = dto.CreatedBy;
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Fetch updated with User
            var updated = await _context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.LogId == entity.LogId)
                .Select(a => new AuditLogDto
                {
                    LogId = a.LogId,
                    UserId = a.UserId,
                    Username = a.User.Username,
                    Action = a.Action,
                    TableName = a.TableName,
                    RecordId = a.RecordId,
                    OldValue = a.OldValue,
                    NewValue = a.NewValue,
                    IpAddress = a.IpAddress,
                    RecordStatus = a.RecordStatus
                })
                .FirstOrDefaultAsync();

            return updated;
        }


        public async Task<bool> DeleteAsync(long logId)
        {
            var entity = await _context.AuditLogs.FindAsync(logId);
            if (entity == null) return false;

            _context.AuditLogs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
