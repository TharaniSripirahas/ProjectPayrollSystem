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
    public class UserService : IUserService
    {
        private readonly PayrollDbContext _context;

        public UserService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    EmployeeId = u.EmployeeId,
                    RoleId = u.RoleId,
                    RoleName = u.Role.RoleName,
                    IsActive = u.IsActive,
                    LastLogin = u.LastLogin,
                    RecordStatus = u.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(long userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                EmployeeId = user.EmployeeId,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
                IsActive = user.IsActive,
                LastLogin = user.LastLogin,
                RecordStatus = user.RecordStatus
            };
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hashedPassword,
                EmployeeId = dto.EmployeeId,
                RoleId = dto.RoleId,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var role = await _context.UserRoles.FindAsync(user.RoleId);

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                EmployeeId = user.EmployeeId,
                RoleId = user.RoleId,
                RoleName = role?.RoleName ?? string.Empty,
                IsActive = user.IsActive,
                LastLogin = user.LastLogin,
                RecordStatus = user.RecordStatus
            };
        }

        public async Task<UserDto?> UpdateAsync(long userId, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            user.Username = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }
            user.RoleId = dto.RoleId;
            user.IsActive = dto.IsActive;
            user.RecordStatus = dto.RecordStatus;
            user.LastModifiedBy = dto.LastModifiedBy;
            user.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var role = await _context.UserRoles.FindAsync(user.RoleId);

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                EmployeeId = user.EmployeeId,
                RoleId = user.RoleId,
                RoleName = role?.RoleName ?? string.Empty,
                IsActive = user.IsActive,
                LastLogin = user.LastLogin,
                RecordStatus = user.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
