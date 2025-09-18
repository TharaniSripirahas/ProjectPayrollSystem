using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class SalaryComponent
{
    public long ComponentId { get; set; }

    public string ComponentName { get; set; } = null!;

    public long ComponentType { get; set; }

    public int? IsTaxable { get; set; }

    public int? IsStatutory { get; set; }

    public string? Description { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<TemplateComponent> TemplateComponentComponents { get; set; } = new List<TemplateComponent>();

    public virtual ICollection<TemplateComponent> TemplateComponentSalaryComponentModelComponents { get; set; } = new List<TemplateComponent>();
}
