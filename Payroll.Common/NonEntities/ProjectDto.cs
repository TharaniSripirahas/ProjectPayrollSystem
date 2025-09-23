using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class ProjectDto
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? ManagerName { get; set; }
        public string ClientName { get; set; } = null!;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? IsActive { get; set; }
        public long ManagerId { get; set; }
        public int RecordStatus { get; set; }
    }

    public class EmployeeProjectMappingDto
    {
        public long MappingId { get; set; }
        public long EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ProjectName { get; set; }
        public long ProjectId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? IsCurrent { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class PerformanceMetricDto
    {
        public long MetricId { get; set; }
        public string MetricName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public int? IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class ProjectPerformanceDto
    {
        public long PerformanceId { get; set; }
        public long EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public long ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public long MetricId { get; set; }
        public string? MetricName { get; set; }
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
    }
}
