using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Designation
{
    public long DesignationId { get; set; }

    public string DesignationName { get; set; } = null!;

    public string? Description { get; set; }

    public long DepartmentId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
