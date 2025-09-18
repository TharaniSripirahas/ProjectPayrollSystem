using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class AttendanceLog
{
    public long AttendanceId { get; set; }

    public long EmployeeName { get; set; }

    public DateOnly LogDate { get; set; }

    public DateTime? PunchIn { get; set; }

    public DateTime? PunchOut { get; set; }

    public long ShiftId { get; set; }

    public int? IsLate { get; set; }

    public int? LateMinutes { get; set; }

    public int? EarlyDepartureMinutes { get; set; }

    public long ApprovedBy { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee EmployeeNameNavigation { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
