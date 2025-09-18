using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class LeaveRequest
{
    public long LeaveId { get; set; }

    public long EmployeeName { get; set; }

    public long LeaveTypeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal DaysCount { get; set; }

    public string Reason { get; set; } = null!;

    public int Status { get; set; }

    public long ApprovedBy { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee EmployeeNameNavigation { get; set; } = null!;

    public virtual LeaveType LeaveType { get; set; } = null!;
}
