using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Permission
{
    public long PermissionId { get; set; }

    public long RoleId { get; set; }

    public string Resource { get; set; } = null!;

    public string Action { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual UserRole Role { get; set; } = null!;
}
