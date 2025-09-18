using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class LeaveType
{
    public long LeaveTypeId { get; set; }

    public string LeaveName { get; set; } = null!;

    public int? IsPaid { get; set; }

    public int? MaxDaysPerYear { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
