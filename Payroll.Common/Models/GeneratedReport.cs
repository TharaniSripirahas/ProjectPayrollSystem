using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class GeneratedReport
{
    public long ReportId { get; set; }

    public string? FilePath { get; set; }

    public string? ReportPeriod { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long GeneratedBy { get; set; }

    public DateTime? GeneratedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee GeneratedByNavigation { get; set; } = null!;
}
