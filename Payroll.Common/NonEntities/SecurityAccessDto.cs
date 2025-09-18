using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class SecurityAccessDto
    {
        // USER ROLE DTOs 
        public class UserRoleDto
        {
            public long RoleId { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateUserRoleDto
        {
            public string RoleName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public long CreatedBy { get; set; }
        }

        public class UpdateUserRoleDto
        {
            public string RoleName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int RecordStatus { get; set; }
            public long? LastModifiedBy { get; set; }
        }

        // USER DTOs 
        public class UserDto
        {
            public long UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public long EmployeeId { get; set; }
            public long RoleId { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public int? IsActive { get; set; }
            public DateTime? LastLogin { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateUserDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty; 
            public long EmployeeId { get; set; }
            public long RoleId { get; set; }
            public int? IsActive { get; set; }
            public long CreatedBy { get; set; }
        }

        public class UpdateUserDto
        {
            public string Username { get; set; } = string.Empty;
            public string? Password { get; set; } 
            public long RoleId { get; set; }
            public int? IsActive { get; set; }
            public int RecordStatus { get; set; }
            public long? LastModifiedBy { get; set; }
        }

        // PERMISSION 
        public class PermissionDto
        {
            public long PermissionId { get; set; }
            public long RoleId { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public string Resource { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
            public int RecordStatus { get; set; }
        }

        public class CreatePermissionDto
        {
            public long RoleId { get; set; }
            public string Resource { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
        }

        public class UpdatePermissionDto : CreatePermissionDto
        {
            public long PermissionId { get; set; }
            public int RecordStatus { get; set; }
        }

        // AUDIT LOG 
        public class AuditLogDto
        {
            public long LogId { get; set; }
            public long UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
            public string TableName { get; set; } = string.Empty;
            public long? RecordId { get; set; }
            public string? OldValue { get; set; }
            public string? NewValue { get; set; }
            public string? IpAddress { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateAuditLogDto
        {
            public long UserId { get; set; }
            public string Action { get; set; } = string.Empty;
            public string TableName { get; set; } = string.Empty;
            public long? RecordId { get; set; }
            public string? OldValue { get; set; }
            public string? NewValue { get; set; }
            public string? IpAddress { get; set; }
            public long CreatedBy { get; set; }
        }

        public class UpdateAuditLogDto : CreateAuditLogDto
        {
            public long LogId { get; set; }
            public int RecordStatus { get; set; }
        }
    }
}
