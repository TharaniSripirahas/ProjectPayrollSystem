using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ReimbursementClaim
{
    public long ClaimId { get; set; }

    public long EmployeeId { get; set; }

    public long TypeId { get; set; }

    public DateTime ClaimDate { get; set; }

    public decimal Amount { get; set; }

    public string? Document { get; set; }

    public long ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public long? PayrollCycleId { get; set; } //FK

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual PayrollCycle? PayrollCycle { get; set; }

    public virtual ReimbursementType Type { get; set; } = null!;

}
