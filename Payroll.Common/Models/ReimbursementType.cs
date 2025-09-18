using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class ReimbursementType
{
    public long TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal MaxAmountPerMonth { get; set; }

    public int IsClaim { get; set; }

    public int? RequiresDocument { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<ReimbursementClaim> ReimbursementClaims { get; set; } = new List<ReimbursementClaim>();
}
