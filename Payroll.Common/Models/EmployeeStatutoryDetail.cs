using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmployeeStatutoryDetail
{
    public long DetailsId { get; set; }

    public long EmployeeId { get; set; }

    public long DeductionId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountDetails { get; set; } = null!;

    public int IsApplicable { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual StatutoryDeduction StatutoryDeduction { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
    //public StatutoryDeduction StatutoryDeduction { get; set; }  // navigation property

}
