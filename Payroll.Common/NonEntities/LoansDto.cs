using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class LoansDto
    {
        public class LoanTypeDto
        {
            public long LoanTypeId { get; set; }
            public string TypeName { get; set; } = string.Empty;
            public decimal? MaxAmount { get; set; }
            public int? MaxTenureMonths { get; set; }
            public decimal? InterestRate { get; set; }
        }

        public class CreateLoanTypeDto
        {
            public string TypeName { get; set; } = string.Empty;
            public decimal? MaxAmount { get; set; }
            public int? MaxTenureMonths { get; set; }
            public decimal? InterestRate { get; set; }
            public long CreatedBy { get; set; }
        }

        public class UpdateLoanTypeDto
        {
            public long LoanTypeId { get; set; }
            public string TypeName { get; set; } = string.Empty;
            public decimal? MaxAmount { get; set; }
            public int? MaxTenureMonths { get; set; }
            public decimal? InterestRate { get; set; }
            public long LastModifiedBy { get; set; }
        }

        // EmployeeLoan DTOs
        public class EmployeeLoanDto
        {
            public long LoanId { get; set; }
            public long EmployeeId { get; set; }
            public string? EmployeeName { get; set; }
            public long LoanTypeId { get; set; }
            public string? LoanTypeName { get; set; }
            public decimal Amount { get; set; }
            public DateTime SanctionDate { get; set; }
            public int TenureMonths { get; set; }
            public decimal InterestRate { get; set; }
            public decimal EmiAmount { get; set; }
            public int Status { get; set; }
            public string Purpose { get; set; } = string.Empty;

        }

        public class CreateEmployeeLoanDto
        {
            public long EmployeeId { get; set; }
            public long LoanTypeId { get; set; }
            public decimal Amount { get; set; }
            public DateTime SanctionDate { get; set; }
            public int TenureMonths { get; set; }
            public decimal InterestRate { get; set; }
            public decimal EmiAmount { get; set; }
            public int Status { get; set; }
            public string Purpose { get; set; } = string.Empty;
            public long CreatedBy { get; set; }
        }

        public class UpdateEmployeeLoanDto
        {
            public long EmployeeId { get; set; }
            public long LoanTypeId { get; set; }
            public long LoanId { get; set; }
            public decimal Amount { get; set; }
            public int TenureMonths { get; set; }
            public decimal InterestRate { get; set; }
            public decimal EmiAmount { get; set; }
            public int Status { get; set; }
            public string Purpose { get; set; } = string.Empty;
            public long LastModifiedBy { get; set; }
        }

        // LoanRepayment DTOs
        public class LoanRepaymentDto
        {
            public long RepaymentId { get; set; }
            public long LoanId { get; set; }
            public string? EmployeeName { get; set; }
            public string? LoanTypeName { get; set; }

            public DateTime PaymentDate { get; set; }

            public long PayrollCycleId { get; set; }
            public string? PayrollCycleName { get; set; }

            public decimal Amount { get; set; }
            public decimal InterestAmount { get; set; }
            public decimal RemainingBalance { get; set; }
            public int RecordStatus { get; set; }
        }

        public class LoanRepaymentCreateDto
        {
            public long LoanId { get; set; }
            public long PayrollCycleId { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Amount { get; set; }
            public decimal InterestAmount { get; set; }
            public decimal RemainingBalance { get; set; }
            public long CreatedBy { get; set; }
        }

        public class LoanRepaymentUpdateDto
        {
            public long RepaymentId { get; set; }
            public decimal Amount { get; set; }
            public decimal InterestAmount { get; set; }
            public decimal RemainingBalance { get; set; }
            public long LastModifiedBy { get; set; }
        }


        // DonationFund DTOs
        public class DonationFundDto
        {
            public long FundId { get; set; }
            public string FundName { get; set; } = null!;
            public string? Description { get; set; }
            public int? IsActive { get; set; }
            public int RecordStatus { get; set; }

            public long CreatedBy { get; set; }

            public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
            public DateTime? LastModifiedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            
        }

        public class DonationFundCreateDto
        {
            public string FundName { get; set; } = null!;

            public string? Description { get; set; }

            public int? IsActive { get; set; } = 1;

            public long CreatedBy { get; set; }

            public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

            public int RecordStatus { get; set; } = 0;
            public DateTime? LastModifiedOn { get; set; }

        }

        public class DonationFundUpdateDto
        {
            public long FundId { get; set; }
            public string FundName { get; set; } = null!;
            public string? Description { get; set; }
            public int? IsActive { get; set; }
            public long LastModifiedBy { get; set; }
            public int RecordStatus { get; set; }
        }

        // EmployeeDonation DTOs
        public class EmployeeDonationDto
        {
            public long DonationId { get; set; }
            public long EmployeeId { get; set; }
            public string? EmployeeName { get; set; }   
            public long FundId { get; set; }
            public string? FundName { get; set; }       
            public decimal Amount { get; set; }
            public DateTime DonationDate { get; set; }
            public string? Cause { get; set; }
            public long? PayrollCycleId { get; set; }
            public string? PayrollCycleName { get; set; } 
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; }
        }

        public class EmployeeDonationCreateDto
        {
            public long EmployeeId { get; set; }
            public long FundId { get; set; }
            public decimal Amount { get; set; }
            public DateTime DonationDate { get; set; }
            public string? Cause { get; set; }
            public long? PayrollCycleId { get; set; }
            public long CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
            public int RecordStatus { get; set; } = 0;
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
        public class EmployeeDonationUpdateDto
        {
            public long DonationId { get; set; }
            public long EmployeeId { get; set; }
            public long FundId { get; set; }
            public decimal Amount { get; set; }
            public DateTime DonationDate { get; set; }
            public string? Cause { get; set; }
            public long? PayrollCycleId { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow;
            public int RecordStatus { get; set; }
        }

        public class CreateEmployeeDonationDto
        {
            public long EmployeeId { get; set; }
            public long FundId { get; set; }
            public decimal Amount { get; set; }
            public DateTime DonationDate { get; set; }
            public string? Cause { get; set; }
            public long? PayrollCycleId { get; set; }
            public long CreatedBy { get; set; }
        }

        public class UpdateEmployeeDonationDto : CreateEmployeeDonationDto
        {
            public int RecordStatus { get; set; }
        }
    }
}
