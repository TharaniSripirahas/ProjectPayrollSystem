using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PayrollRecord
{
    public long RecordId { get; set; }
    public string? RecordName { get; set; }

    public long PayrollCycleId { get; set; }

    public long EmployeeId { get; set; }

    public decimal GrossEarnings { get; set; }

    public decimal TotalDeduction { get; set; }

    public decimal NetPay { get; set; }

    public int PaymentStatus { get; set; }

    public int? PayslipGenerated { get; set; }

    public string? PayslipPath { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<PayrollComponent> PayrollComponents { get; set; } = new List<PayrollComponent>();

    public virtual PayrollCycle PayrollCycle { get; set; } = null!;

    public virtual ICollection<PayslipStorage> PayslipStorages { get; set; } = new List<PayslipStorage>();
}
