using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Project
{
    public long ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? IsActive { get; set; }

    public long ManagerId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<EmployeeProjectMapping> EmployeeProjectMappings { get; set; } = new List<EmployeeProjectMapping>();

    public virtual Employee Manager { get; set; } = null!;

    public virtual ICollection<ProjectPerformance> ProjectPerformances { get; set; } = new List<ProjectPerformance>();
}
