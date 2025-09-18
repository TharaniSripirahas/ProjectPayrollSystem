using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Skill
{
    public long SkillId { get; set; }

    public string SkillName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
}
