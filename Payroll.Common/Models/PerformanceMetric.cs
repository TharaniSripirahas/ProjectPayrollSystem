using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PerformanceMetric
{
    public long MetricId { get; set; }

    public string MetricName { get; set; } = null!;

    public string? Description { get; set; }

    public string Unit { get; set; } = null!;

    public int? IsActive { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<ProjectPerformance> ProjectPerformances { get; set; } = new List<ProjectPerformance>();
}
