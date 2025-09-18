using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class StatutoryDeduction
{
    public long DeductionId { get; set; }

    public string DeductionName { get; set; } = null!;

    public string DeductionCode { get; set; } = null!;

    public string CalculationMethod { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<EmployeeStatutoryDetail> EmployeeStatutoryDetails { get; set; } = new List<EmployeeStatutoryDetail>();
}
