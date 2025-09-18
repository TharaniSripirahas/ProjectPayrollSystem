using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class PayslipsReportingDto
    {
        public class CreatePayslipDto
        {
            public long PayrollId { get; set; }
            public long EmployeeId { get; set; }
            public string FilePath { get; set; } = string.Empty;
            public string FileHash { get; set; } = string.Empty;
            public int IsDelivered { get; set; }
            public string? DeliveryMethod { get; set; }
            public DateTime? DeliveryDate { get; set; }
            public string TdsSheetPath { get; set; } = string.Empty;
        }

        public class PayslipDto
        {
            public long PayslipId { get; set; }
            public long PayrollId { get; set; }
            public string? PayrollName { get; set; }   
            public long EmployeeId { get; set; }
            public string? EmployeeName { get; set; }  
            public string FilePath { get; set; } = string.Empty;
            public string FileHash { get; set; } = string.Empty;
            public DateTime GeneratedAt { get; set; }
            public int IsDelivered { get; set; }
            public string? DeliveryMethod { get; set; }
            public DateTime? DeliveryDate { get; set; }
            public string TdsSheetPath { get; set; } = string.Empty;
            public int RecordStatus { get; set; }
        }

        public class UpdatePayslipDto : CreatePayslipDto
        {
            public long PayslipId { get; set; }
            public int RecordStatus { get; set; }
        }

        public class PayslipAccessLogDto
        {
            public long LogId { get; set; }
            public long PayslipId { get; set; }
            public long AccessedBy { get; set; }
            public string? AccessedByName { get; set; }
            public string? PayslipFilePath { get; set; }
            public DateTime? AccessTime { get; set; }
            public string? IpAddress { get; set; }
            public int RecordStatus { get; set; }

        }

        public class CreatePayslipAccessLogDto
        {
            public long LogId { get; set; }                 
            public long PayslipId { get; set; }
            public long AccessedBy { get; set; }
            public DateTime? AccessTime { get; set; }
            public string? IpAddress { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; }

            public long? EmployeeId { get; set; }
            public long? PayslipStorageId { get; set; }
        }


        public class UpdatePayslipAccessLogDto : CreatePayslipAccessLogDto
        {
            public long LogId { get; set; }
            public int RecordStatus { get; set; }
        }

        public class GeneratedReportDto
        {
            public long ReportId { get; set; }
            public string? FilePath { get; set; }
            public string? ReportPeriod { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public long GeneratedBy { get; set; }
            public string? GeneratedByName { get; set; }

            public DateTime? GeneratedAt { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateGeneratedReportDto
        {
            public long ReportId { get; set; }
            public string? FilePath { get; set; }
            public string? ReportPeriod { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public long GeneratedBy { get; set; }
            public DateTime? GeneratedAt { get; set; }
        }

        public class UpdateGeneratedReportDto : CreateGeneratedReportDto
        {
            public long ReportId { get; set; }
            public int RecordStatus { get; set; }
        }
    }
}
