using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class LoanRepayment
{
    public long RepaymentId { get; set; }

    public long LoanId { get; set; }

    public DateTime PaymentDate { get; set; }

    public long PayrollCycleId { get; set; }

    public decimal Amount { get; set; }

    public decimal InterestAmount { get; set; }

    public decimal RemainingBalance { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual EmployeeLoan Loan { get; set; } = null!;

    public virtual PayrollCycle PayrollCycle { get; set; } = null!;
}
