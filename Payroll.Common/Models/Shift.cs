using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Shift
{
    public long ShiftId { get; set; }

    public string ShiftName { get; set; } = null!;

    public TimeOnly ShiftTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int IsNightShift { get; set; }

    public int IsWeekendShift { get; set; }

    public int IsHolidayShift { get; set; }

    public decimal? AllowancePercentage { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
}
