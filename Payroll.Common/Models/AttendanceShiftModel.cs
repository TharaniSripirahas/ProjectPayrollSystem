using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Models
{
        public class Shift
        {
            public long ShiftId { get; set; }
            public string ShiftName { get; set; } = string.Empty;
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

            // Navigation
           public ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
        }

        public class AttendanceLog
        {
            [Key]
            public long AttendanceId { get; set; }

            [ForeignKey("Employee")]
            public long EmployeeName { get; set; }   // FK -> Employees.EmployeeId

            [Column(TypeName = "date")]
            public DateOnly LogDate { get; set; }

            public DateTime? PunchIn { get; set; }
            public DateTime? PunchOut { get; set; }

            [ForeignKey("Shift")]
            public long ShiftId { get; set; }

            public int? IsLate { get; set; }
            public int? LateMinutes { get; set; }
            public int? EarlyDepartureMinutes { get; set; }

            public long ApprovedBy { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;

            // Navigation
            public Employees Employee { get; set; } = null!;
            public Shift Shift { get; set; } = null!;
        }


        public class LeaveType
        {
            public long LeaveTypeId { get; set; }
            public string LeaveName { get; set; } = string.Empty;
            public int? IsPaid { get; set; }
            public int? MaxDaysPerYear { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; }

            // Navigation
            public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        }

    public class LeaveRequest
    {
        [Key]
        public long LeaveId { get; set; }

        [ForeignKey("Employee")]
        public long EmployeeName { get; set; }

        [ForeignKey("LeaveType")]
        public long LeaveTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateOnly StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateOnly EndDate { get; set; }

        public decimal DaysCount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int Status { get; set; }

        public long ApprovedBy { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;

        // Navigation
        public Employees Employee { get; set; } = null!;
        public LeaveType LeaveType { get; set; } = null!;
    }
}
