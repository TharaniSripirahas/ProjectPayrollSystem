using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class DonationFund
{
    public long FundId { get; set; }

    public string FundName { get; set; } = null!;

    public string? Description { get; set; }

    public int? IsActive { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<EmployeeDonation> EmployeeDonationDonationFundFunds { get; set; } = new List<EmployeeDonation>();

    public virtual ICollection<EmployeeDonation> EmployeeDonationFunds { get; set; } = new List<EmployeeDonation>();
}
