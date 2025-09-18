using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ProjectPerformance
{
    public long PerformanceId { get; set; }

    public long EmployeeId { get; set; }

    public long ProjectId { get; set; }

    public long MetricId { get; set; }

    public DateOnly PeriodStart { get; set; }

    public DateOnly PeriodEnd { get; set; }

    public decimal AchievedValue { get; set; }

    public decimal TargetValue { get; set; }

    public decimal? BonusAmount { get; set; }

    public long ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual PerformanceMetric Metric { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
