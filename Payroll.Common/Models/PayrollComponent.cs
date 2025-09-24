using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PayrollComponent
{
    public long PayrollComponentId { get; set; }

    public long RecordId { get; set; }

    public long ComponentId { get; set; }

    public decimal Amount { get; set; }

    public int IsEarning { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual PayrollRecord Record { get; set; } = null!;
    public virtual SalaryComponent Component { get; set; } = null!;
}
