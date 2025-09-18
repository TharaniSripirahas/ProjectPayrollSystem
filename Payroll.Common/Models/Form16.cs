using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Form16
{
    public long FormId { get; set; }

    public long EmployeeId { get; set; }

    public string FinancialYear { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime GeneratedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
