using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PayslipAccessLog
{
    public long LogId { get; set; }

    public long PayslipId { get; set; }  // FK

    public long AccessedBy { get; set; }  // FK

    public DateTime? AccessTime { get; set; }

    public string? IpAddress { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee AccessedByNavigation { get; set; } = null!;

    public virtual PayslipStorage Payslip { get; set; } = null!;
}
