using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PayrollCycle
{
    public long PayrollCycleId { get; set; }

    public string PayrollCycleName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateOnly PaymentDate { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<LoanRepayment> LoanRepayments { get; set; } = new List<LoanRepayment>();

    public virtual ICollection<PayrollRecord> PayrollRecords { get; set; } = new List<PayrollRecord>();

    public virtual ICollection<PayslipNotification> PayslipNotifications { get; set; } = new List<PayslipNotification>();

    public virtual ICollection<ReimbursementClaim> ReimbursementClaims { get; set; } = new List<ReimbursementClaim>();
}
