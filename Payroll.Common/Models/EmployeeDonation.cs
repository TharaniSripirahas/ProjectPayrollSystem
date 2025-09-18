using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmployeeDonation
{
    public long DonationId { get; set; }

    public long EmployeeId { get; set; }

    public long FundId { get; set; }

    public decimal Amount { get; set; }

    public DateTime DonationDate { get; set; }

    public string? Cause { get; set; }

    public long? PayrollCycleId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public long? DonationFundFundId { get; set; }

    public virtual DonationFund? DonationFundFund { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual DonationFund Fund { get; set; } = null!;

    public PayrollCycle? PayrollCycle { get; set; }
}
