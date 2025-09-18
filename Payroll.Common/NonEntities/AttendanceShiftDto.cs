using Payroll.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.Enums.AppEnums;

namespace Payroll.Common.NonEntities
{
    public class ShiftDto
    {
        public long ShiftId { get; set; }
        public string ShiftName { get; set; } = string.Empty;
        public TimeOnly ShiftTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int IsNightShift { get; set; }
        public int IsWeekendShift { get; set; }
        public int IsHolidayShift { get; set; }
        public decimal? AllowancePercentage { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }

    public class AttendanceLogDto
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
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }

    public class LeaveTypeDto
    {
        public long LeaveTypeId { get; set; }
        public string LeaveName { get; set; } = string.Empty;
        public int? IsPaid { get; set; }
        public int? MaxDaysPerYear { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }

    public class LeaveRequestDto
    {
        public long LeaveId { get; set; }
        public long EmployeeName { get; set; }
        public long LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DaysCount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }

}
