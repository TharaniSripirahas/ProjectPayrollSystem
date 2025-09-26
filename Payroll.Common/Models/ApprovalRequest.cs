using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ApprovalRequest
{
    public long RequestId { get; set; }

    public long WorkflowId { get; set; }

    public long CurrentLevelId { get; set; }

    public string? EntityTable { get; set; }

    public long? RequesterId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<ApprovalAction> ApprovalActions { get; set; } = new List<ApprovalAction>();

    public virtual ApprovalLevel CurrentLevel { get; set; } = null!;

    public virtual ApprovalWorkflow Workflow { get; set; } = null!;
    public virtual Employee? Requester { get; set; }

}
