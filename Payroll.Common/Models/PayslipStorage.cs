using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class PayslipStorage
{
    public long PayslipId { get; set; }

    public long PayrollId { get; set; }

    public long EmployeeId { get; set; }

    public string FilePath { get; set; } = null!;

    public string FileHash { get; set; } = null!;

    public DateTime GeneratedAt { get; set; }

    public int IsDelivered { get; set; }

    public string? DeliveryMethod { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string TdsSheetPath { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual PayrollRecord Payroll { get; set; } = null!;

    public virtual ICollection<PayslipAccessLog> PayslipAccessLogs { get; set; } = new List<PayslipAccessLog>();
    
    public virtual Employee Employee { get; set; } = null!;
}
