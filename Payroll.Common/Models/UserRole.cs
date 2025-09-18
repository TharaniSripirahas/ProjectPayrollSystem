using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class UserRole
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
