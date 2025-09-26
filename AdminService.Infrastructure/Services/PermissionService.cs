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
    public class PermissionService : IPermissionService
    {
        private readonly PayrollDbContext _context;

        public PermissionService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllAsync()
        {
            return await _context.Permissions
                .Include(p => p.Role)
                .Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    RoleId = p.RoleId,
                    RoleName = p.Role.RoleName,
                    Resource = p.Resource,
                    Action = p.Action,
                    RecordStatus = p.RecordStatus,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn

                })
                .ToListAsync();
        }

        public async Task<PermissionDto?> GetByIdAsync(long permissionId)
        {
            var p = await _context.Permissions
                .Include(p => p.Role)
                .FirstOrDefaultAsync(x => x.PermissionId == permissionId);

            if (p == null) return null;

            return new PermissionDto
            {
                PermissionId = p.PermissionId,
                RoleId = p.RoleId,
                RoleName = p.Role.RoleName,
                Resource = p.Resource,
                Action = p.Action,
                RecordStatus = p.RecordStatus,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn
            };
        }

        public async Task<PermissionDto> CreateAsync(CreatePermissionDto dto)
        {
            var now = DateTime.UtcNow;

            var entity = new Permission
            {
                RoleId = dto.RoleId,
                Resource = dto.Resource,
                Action = dto.Action,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1,
                CreatedBy = 1,
                LastModifiedOn = now,
            };

            _context.Permissions.Add(entity);
            await _context.SaveChangesAsync();

            return new PermissionDto
            {
                PermissionId = entity.PermissionId,
                RoleId = entity.RoleId,
                RoleName = (await _context.UserRoles.FindAsync(entity.RoleId))?.RoleName ?? "",
                Resource = entity.Resource,
                Action = entity.Action,
                RecordStatus = entity.RecordStatus,
                LastModifiedOn = entity.LastModifiedOn

            };
        }

        public async Task<PermissionDto?> UpdateAsync(long permissionId, UpdatePermissionDto dto)
        {
            var entity = await _context.Permissions.FindAsync(permissionId);
            if (entity == null) return null;

            entity.RoleId = dto.RoleId;
            entity.Resource = dto.Resource;
            entity.Action = dto.Action;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = 1; 
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new PermissionDto
            {
                PermissionId = entity.PermissionId,
                RoleId = entity.RoleId,
                RoleName = (await _context.UserRoles.FindAsync(entity.RoleId))?.RoleName ?? "",
                Resource = entity.Resource,
                Action = entity.Action,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long permissionId)
        {
            var entity = await _context.Permissions.FindAsync(permissionId);
            if (entity == null) return false;

            _context.Permissions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
