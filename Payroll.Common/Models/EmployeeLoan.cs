using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmployeeLoan
{
    public long LoanId { get; set; }

    public long EmployeeId { get; set; }

    public long LoanTypeId { get; set; }

    public decimal Amount { get; set; }

    public DateTime SanctionDate { get; set; }

    public int TenureMonths { get; set; }

    public decimal InterestRate { get; set; }

    public decimal EmiAmount { get; set; }

    public int Status { get; set; }

    public string Purpose { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<LoanRepayment> LoanRepayments { get; set; } = new List<LoanRepayment>();

    public virtual LoanType LoanType { get; set; } = null!;
}
