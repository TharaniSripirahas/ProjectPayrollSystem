using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    // ReimbursementType DTOs 
    public class ReimbursementTypeDto
    {
        public long TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal MaxAmountPerMonth { get; set; }
        public int IsClaim { get; set; }
        public int? RequiresDocument { get; set; }
        public int RecordStatus { get; set; }
    }

    public class CreateReimbursementTypeDto
    {
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal MaxAmountPerMonth { get; set; }
        public int IsClaim { get; set; }
        public int? RequiresDocument { get; set; }
    }

    public class UpdateReimbursementTypeDto : CreateReimbursementTypeDto
    {
        public long TypeId { get; set; }
        public int RecordStatus { get; set; }
    }

    // ReimbursementClaim DTOs
    public class ReimbursementClaimDto
    {
        public long ClaimId { get; set; }
        public long EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public long TypeId { get; set; }
        public string? TypeName { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal Amount { get; set; }
        public string? Document { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public long? PayrollCycleId { get; set; }
    }

    public class CreateReimbursementClaimDto
    {
        public long EmployeeId { get; set; }
        public long TypeId { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal Amount { get; set; }
        public string? Document { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public long? PayrollCycleId { get; set; }
    }

    public class UpdateReimbursementClaimDto
    {
        public long EmployeeId { get; set; }
        public long TypeId { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal Amount { get; set; }
        public string? Document { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public long? PayrollCycleId { get; set; }
    }

    // PayrollCycle DTOs 
    public class PayrollCycleDto
    {
        public long PayrollCycleId { get; set; }
        public string PayrollCycleName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ProcessedAt { get; set; }

        public int PayrollRecordsCount { get; set; }
        public int ReimbursementClaimsCount { get; set; }
        public int PayslipNotificationsCount { get; set; }
    }

    public class CreatePayrollCycleDto
    {
        public string PayrollCycleName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class UpdatePayrollCycleDto : CreatePayrollCycleDto
    {
        public long PayrollCycleId { get; set; }
    }

    // PayrollRecord DTOs 
    public class PayrollRecordDto
    {
        public long RecordId { get; set; }
        public long PayrollCycleId { get; set; }
        public long EmployeeId { get; set; }
        public decimal GrossEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPay { get; set; }
        public int PaymentStatus { get; set; }
        public int? PayslipGenerated { get; set; }
        public string? PayslipPath { get; set; }
        public int RecordStatus { get; set; }

        public string? PayrollCycleName { get; set; }
        public string? EmployeeName { get; set; }
    }

    public class CreatePayrollRecordDto
    {
        public long PayrollCycleId { get; set; }
        public long EmployeeId { get; set; }
        public decimal GrossEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPay { get; set; }
        public int PaymentStatus { get; set; }
        public int? PayslipGenerated { get; set; }
        public string? PayslipPath { get; set; }
    }

    public class UpdatePayrollRecordDto
    {
        public long PayrollCycleId { get; set; }
        public long EmployeeId { get; set; }
        public decimal GrossEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPay { get; set; }
        public int PaymentStatus { get; set; }
        public int? PayslipGenerated { get; set; }
        public string? PayslipPath { get; set; }
        public int RecordStatus { get; set; }
    }

    // PayrollComponent DTOs 
    public class PayrollComponentDto
    {
        public long PayrollComponentId { get; set; }
        public long RecordId { get; set; }
        public long ComponentId { get; set; }
        public decimal Amount { get; set; }
        public int IsEarning { get; set; }
        public int RecordStatus { get; set; }
        public string? PayrollCycleName { get; set; }
        public string? EmployeeName { get; set; }
    }

    public class CreatePayrollComponentDto
    {
        public long RecordId { get; set; }
        public long ComponentId { get; set; }
        public decimal Amount { get; set; }
        public int IsEarning { get; set; }
    }

    public class UpdatePayrollComponentDto
    {
        public long RecordId { get; set; }
        public long ComponentId { get; set; }
        public decimal Amount { get; set; }
        public int IsEarning { get; set; }
        public int RecordStatus { get; set; }
    }
    //  PayslipNotification DTOs 
    public class PayslipNotificationDto
    {
        public long NotificationId { get; set; }
        public long EmployeeId { get; set; }
        public long PayrollCycleId { get; set; }
        public string NotificationMethod { get; set; } = string.Empty;
        public int? Status { get; set; }

        public string? PayrollCycleName { get; set; }
        public string? EmployeeName { get; set; }

        public int RecordStatus { get; set; }
    }

    public class CreatePayslipNotificationDto
    {
        public long EmployeeId { get; set; }
        public long PayrollCycleId { get; set; }
        public string NotificationMethod { get; set; } = string.Empty;
        public int? Status { get; set; }
    }

    public class UpdatePayslipNotificationDto
    {
        public long EmployeeId { get; set; }
        public long PayrollCycleId { get; set; }
        public string NotificationMethod { get; set; } = string.Empty;
        public int? Status { get; set; }
    }




    //public class ReimbursementTypeDto
    //{
    //    public long TypeId { get; set; }
    //    public string TypeName { get; set; } = string.Empty;
    //    public string? Description { get; set; }
    //    public decimal MaxAmountPerMonth { get; set; }
    //    public int IsClaim { get; set; }
    //    public int? RequiresDocument { get; set; }
    //    public int RecordStatus { get; set; }
    //}

    //public class CreateReimbursementTypeDto
    //{
    //    public string TypeName { get; set; } = string.Empty;
    //    public string? Description { get; set; }
    //    public decimal MaxAmountPerMonth { get; set; }
    //    public int IsClaim { get; set; }
    //    public int? RequiresDocument { get; set; }
    //}

    //public class UpdateReimbursementTypeDto : CreateReimbursementTypeDto
    //{
    //    public long TypeId { get; set; }
    //    public int RecordStatus { get; set; }
    //}

    //public class PayrollCycleDto
    //{
    //    public long PayrollCycleId { get; set; }
    //    public string PayrollCycleName { get; set; } = string.Empty;
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public DateTime PaymentDate { get; set; }
    //}

    //public class CreatePayrollCycleDto
    //{
    //    public string PayrollCycleName { get; set; } = string.Empty;
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public DateTime PaymentDate { get; set; }
    //}

    //public class UpdatePayrollCycleDto : CreatePayrollCycleDto
    //{
    //    public long PayrollCycleId { get; set; }
    //}

    //public class PayrollRecordDto
    //{
    //    public long RecordId { get; set; }
    //    public long PayrollCycleId { get; set; }
    //    public long EmployeeId { get; set; }
    //    public decimal GrossEarnings { get; set; }
    //    public decimal TotalDeduction { get; set; }
    //    public decimal NetPay { get; set; }
    //    public int PaymentStatus { get; set; }
    //}

    //public class CreatePayrollRecordDto
    //{
    //    public long PayrollCycleId { get; set; }
    //    public long EmployeeId { get; set; }
    //    public decimal GrossEarnings { get; set; }
    //    public decimal TotalDeduction { get; set; }
    //    public decimal NetPay { get; set; }
    //    public int PaymentStatus { get; set; }
    //}

    //public class UpdatePayrollRecordDto : CreatePayrollRecordDto
    //{
    //    public long RecordId { get; set; }
    //}

    //public class PayrollComponentDto
    //{
    //    public long PayrollComponentId { get; set; }
    //    public long RecordId { get; set; }
    //    public long ComponentId { get; set; }
    //    public decimal Amount { get; set; }
    //    public int IsEarning { get; set; }
    //}

    //public class CreatePayrollComponentDto
    //{
    //    public long RecordId { get; set; }
    //    public long ComponentId { get; set; }
    //    public decimal Amount { get; set; }
    //    public int IsEarning { get; set; }
    //}

    //public class UpdatePayrollComponentDto : CreatePayrollComponentDto
    //{
    //    public long PayrollComponentId { get; set; }
    //}

    //public class PayslipNotificationDto
    //{
    //    public long NotificationId { get; set; }
    //    public long EmployeeId { get; set; }
    //    public long PayrollCycleId { get; set; }
    //    public string NotificationMethod { get; set; } = string.Empty;
    //    public int? Status { get; set; }
    //}

    //public class CreatePayslipNotificationDto
    //{
    //    public long EmployeeId { get; set; }
    //    public long PayrollCycleId { get; set; }
    //    public string NotificationMethod { get; set; } = string.Empty;
    //    public int? Status { get; set; }
    //}

    //public class UpdatePayslipNotificationDto : CreatePayslipNotificationDto
    //{
    //    public long NotificationId { get; set; }
    //}
}
