using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class TaxDeclaration
{
    public long DeclarationId { get; set; }

    public long EmployeeId { get; set; }

    public string FinancialYear { get; set; } = null!;

    public decimal DeclaredAmount { get; set; }

    public decimal VerifiedAmount { get; set; }

    public int Status { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public long VerifiedBy { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
