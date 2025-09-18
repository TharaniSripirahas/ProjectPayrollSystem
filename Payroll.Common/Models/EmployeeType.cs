using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmployeeType
{
    public long EmployeeTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<Employee> EmployeeEmployeeTypes { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> EmployeeEmploymentTypeNavigations { get; set; } = new List<Employee>();

    public virtual ICollection<SalaryTemplate> SalaryTemplates { get; set; } = new List<SalaryTemplate>();
}
