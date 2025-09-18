using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class TemplateComponent
{
    public long TemplateComponentId { get; set; }

    public long TemplateId { get; set; }

    public long ComponentId { get; set; }

    public long CalculationType { get; set; }

    public decimal? Value { get; set; }

    public decimal? MaxLimit { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public long? SalaryComponentModelComponentId { get; set; }

    public virtual SalaryComponent Component { get; set; } = null!;

    public virtual SalaryComponent? SalaryComponentModelComponent { get; set; }

    public virtual SalaryTemplate Template { get; set; } = null!;
}
