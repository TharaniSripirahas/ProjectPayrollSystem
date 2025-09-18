using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class EmployeeSkill
{
    public long EmployeeSkillId { get; set; }

    public long EmployeeId { get; set; }

    public long SkillId { get; set; }

    public string ProficiencyLevel { get; set; } = null!;

    public int Certificate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
