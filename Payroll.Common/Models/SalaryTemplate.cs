using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class SalaryTemplate
{
    public long TemplateId { get; set; }

    public string TemplateName { get; set; } = null!;

    public long EmployeeTypeId { get; set; }

    public string? Description { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<EmpSalaryStructure> EmpSalaryStructureSalaryTemplateTemplates { get; set; } = new List<EmpSalaryStructure>();

    public virtual ICollection<EmpSalaryStructure> EmpSalaryStructureTemplates { get; set; } = new List<EmpSalaryStructure>();

    public virtual EmployeeType EmployeeType { get; set; } = null!;

    public virtual ICollection<TemplateComponent> TemplateComponents { get; set; } = new List<TemplateComponent>();
}
