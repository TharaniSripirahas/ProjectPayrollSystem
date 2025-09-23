using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmpSalaryStructure
{
    public long StructureId { get; set; }

    public long EmployeeId { get; set; }  // FK now

    public long TemplateId { get; set; }

    public decimal BasicSalary { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public int? IsCurrent { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public long? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;  
    public virtual SalaryTemplate Template { get; set; } = null!;
}
