using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class DeductionsComplianceDto
    {
        
        // StatutoryDeduction DTOs
      
        public class StatutoryDeductionDto
        {
            public long DeductionId { get; set; }
            public string DeductionName { get; set; } = string.Empty;
            public string DeductionCode { get; set; } = string.Empty;
            public string CalculationMethod { get; set; } = string.Empty;

            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; }

            public int EmployeeStatutoryDetailsCount { get; set; }
        }

        public class CreateStatutoryDeductionDto
        {
            public string DeductionName { get; set; } = string.Empty;
            public string DeductionCode { get; set; } = string.Empty;
            public string CalculationMethod { get; set; } = string.Empty;

            public long CreatedBy { get; set; } = 1; 
            public DateTime? CreatedOn { get; set; } = null;
            public long? LastModifiedBy { get; set; } = null;
            public DateTime? LastModifiedOn { get; set; } = null;
            public int RecordStatus { get; set; } = 1;
        }


        public class UpdateStatutoryDeductionDto
        {
            public string DeductionName { get; set; } = string.Empty;
            public string DeductionCode { get; set; } = string.Empty;
            public string CalculationMethod { get; set; } = string.Empty;
            public int RecordStatus { get; set; }
        }
       
        // EmployeeStatutoryDetail DTOs
        public class EmployeeStatutoryDetailDto
        {
            public long DetailsId { get; set; }
            public string? EmployeeName { get; set; }
            public long EmployeeId { get; set; }
            public long DeductionId { get; set; }
            public string? DeductionName { get; set; }
            public string AccountNumber { get; set; } = string.Empty;
            public string AccountDetails { get; set; } = string.Empty;
            public int IsApplicable { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateEmployeeStatutoryDetailDto
        {
            public long DetailsId { get; set; }
            public long EmployeeId { get; set; }
            public long DeductionId { get; set; }
            public string AccountNumber { get; set; } = string.Empty;
            public string AccountDetails { get; set; } = string.Empty;
            public int IsApplicable { get; set; }
            public int RecordStatus { get; set; }
            public long CreatedBy { get; set; } = 1; 
            public DateTime? CreatedOn { get; set; } = null;
            public long? LastModifiedBy { get; set; } = null;
            public DateTime? LastModifiedOn { get; set; } = null;
        }

        public class UpdateEmployeeStatutoryDetailDto : CreateEmployeeStatutoryDetailDto
        {
            public long DetailsId { get; set; }
            public int RecordStatus { get; set; }
        }

        // TaxDeclaration DTOs
  
        public class TaxDeclarationDto
        {
            public long DeclarationId { get; set; }
            public long EmployeeId { get; set; }
            public string EmployeeName { get; set; } = string.Empty;
            public string FinancialYear { get; set; } = string.Empty;
            public decimal DeclaredAmount { get; set; }
            public decimal VerifiedAmount { get; set; }
            public int Status { get; set; }
            public DateTime? SubmittedAt { get; set; }
            public long VerifiedBy { get; set; }
            public DateTime? VerifiedAt { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateTaxDeclarationDto
        {
            public long DeclarationId { get; set; } = 0; 
            public long EmployeeId { get; set; }
            public string FinancialYear { get; set; } = string.Empty;
            public decimal DeclaredAmount { get; set; }
            public decimal VerifiedAmount { get; set; } = 0;
            public int Status { get; set; } = 0;
            public DateTime? SubmittedAt { get; set; } = null;
            public long VerifiedBy { get; set; } = 0;
            public DateTime? VerifiedAt { get; set; } = null;
            public long CreatedBy { get; set; } = 1; 
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
            public long? LastModifiedBy { get; set; } = null;
            public DateTime? LastModifiedOn { get; set; } = null;
            public int RecordStatus { get; set; } = 1;
        }

        public class UpdateTaxDeclarationDto : CreateTaxDeclarationDto
        {
            public long DeclarationId { get; set; }
            public decimal VerifiedAmount { get; set; }
            public int Status { get; set; }
            public DateTime? SubmittedAt { get; set; }
            public long VerifiedBy { get; set; }
            public DateTime? VerifiedAt { get; set; }
            public int RecordStatus { get; set; }
        }

       
        // Form16 DTOs
    
        public class Form16Dto
        {
            public long FormId { get; set; }
            public long EmployeeId { get; set; }
            public string? EmployeeName { get; set; }
            public string FinancialYear { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
            public DateTime GeneratedAt { get; set; }
            public int RecordStatus { get; set; }
        }

        public class CreateForm16Dto
        {
            public long EmployeeId { get; set; }
            public string FinancialYear { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;

   
            public long CreatedBy { get; set; } = 0;
            public DateTime? CreatedOn { get; set; } = null;
            public long? LastModifiedBy { get; set; } = null;
            public DateTime? LastModifiedOn { get; set; } = null;
            public int RecordStatus { get; set; } = 1; 
        }

        public class UpdateForm16Dto : CreateForm16Dto
        {
            public long FormId { get; set; }
            public int RecordStatus { get; set; }
        }
    }
}
