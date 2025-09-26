using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ApprovalLevel
{
    public long LevelId { get; set; }
    public string? LevelName { get; set; }
    public long WorkflowId { get; set; }

    public int LevelNumber { get; set; }

    public long? ApproverId { get; set; }
    public long ApproverRoleId { get; set; }

    public int? IsFinalApproval { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<ApprovalAction> ApprovalActions { get; set; } = new List<ApprovalAction>();

    public virtual ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();

    public virtual ApprovalWorkflow Workflow { get; set; } = null!;
    public virtual UserRole ApproverRole { get; set; } = null!;
    public virtual Employee? Approver { get; set; }

}
