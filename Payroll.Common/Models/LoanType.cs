using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class LoanType
{
    public long LoanTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public decimal? MaxAmount { get; set; }

    public int? MaxTenureMonths { get; set; }

    public decimal? InterestRate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<EmployeeLoan> EmployeeLoans { get; set; } = new List<EmployeeLoan>();
}
