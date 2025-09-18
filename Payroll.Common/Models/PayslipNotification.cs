using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PayslipNotification
{
    public long NotificationId { get; set; }

    public long EmployeeId { get; set; }

    public long PayrollCycleId { get; set; }

    public string NotificationMethod { get; set; } = null!;

    public int? Status { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual PayrollCycle PayrollCycle { get; set; } = null!;
}
