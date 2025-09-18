using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class User
{
    public long UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public long EmployeeId { get; set; }

    public long RoleId { get; set; }

    public int? IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual UserRole Role { get; set; } = null!;
}
