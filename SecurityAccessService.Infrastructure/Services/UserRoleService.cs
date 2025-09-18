using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using SecurityAccess.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace SecurityAccess.Infrastructure.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly PayrollDbContext _context;

        public UserRoleService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRoleDto>> GetAllAsync()
        {
            return await _context.UserRoles
                .Select(r => new UserRoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    Description = r.Description,
                    RecordStatus = r.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<UserRoleDto?> GetByIdAsync(long roleId)
        {
            var role = await _context.UserRoles.FindAsync(roleId);
            if (role == null) return null;

            return new UserRoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                RecordStatus = role.RecordStatus
            };
        }

        public async Task<UserRoleDto> CreateAsync(CreateUserRoleDto dto)
        {
            var role = new UserRole
            {
                RoleName = dto.RoleName,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.UserRoles.Add(role);
            await _context.SaveChangesAsync();

            return new UserRoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                RecordStatus = role.RecordStatus
            };
        }

        public async Task<UserRoleDto?> UpdateAsync(long roleId, UpdateUserRoleDto dto)
        {
            var role = await _context.UserRoles.FindAsync(roleId);
            if (role == null) return null;

            role.RoleName = dto.RoleName;
            role.Description = dto.Description;
            role.RecordStatus = dto.RecordStatus;
            role.LastModifiedBy = dto.LastModifiedBy;
            role.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UserRoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                RecordStatus = role.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long roleId)
        {
            var role = await _context.UserRoles.FindAsync(roleId);
            if (role == null) return false;

            _context.UserRoles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
