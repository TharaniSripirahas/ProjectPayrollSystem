using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmployeeProjectMapping
{
    public long MappingId { get; set; }

    public long EmployeeId { get; set; }

    public long ProjectId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? IsCurrent { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
