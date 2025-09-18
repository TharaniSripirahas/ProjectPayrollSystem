using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ApprovalWorkflow
{
    public long WorkflowId { get; set; }

    public string WorkflowName { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public int IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<ApprovalLevel> ApprovalLevels { get; set; } = new List<ApprovalLevel>();

    public virtual ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();
}
