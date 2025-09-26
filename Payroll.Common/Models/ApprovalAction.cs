using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ApprovalAction
{
    public long ActionId { get; set; }

    public long RequestId { get; set; }

    public long LevelId { get; set; }

    public long ApproverId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? Comments { get; set; }

    public DateTime ActionDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ApprovalLevel Level { get; set; } = null!;
    public virtual ApprovalRequest Request { get; set; } = null!;
    public virtual Employee? Approver { get; set; }

}
