using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Models
{
    public class Employees
    {
        [Key]
        public long EmployeeId { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }
        public long DepartmentId { get; set; }
        public long DesignationId { get; set; }
        public string? SkillLevel { get; set; }
        public string? TechnologyTags { get; set; }
        public long EmploymentType { get; set; }
        public DateOnly JoinDate { get; set; }
        public DateOnly? ExitDate { get; set; }

        [Required, MaxLength(50)]
        public string BankName { get; set; } = string.Empty;

        [Required]
        public byte[] BankAccountNumber { get; set; } = Array.Empty<byte>();

        [Required, MaxLength(15)]
        public string IfscCode { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string PfNumber { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string EsiNumber { get; set; } = string.Empty;

        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public object UserRoles { get; internal set; }
    }

    public class EmployeeType
    {
        public long EmployeeTypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }

    }

    public class Department
    {

        [Key]
        public long DepartmentId { get; set; }
        [Required, MaxLength(50)]
        public string DepartmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? ManagerId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;
    }

    public class Designation
    {
        [Key]
        public long DesignationId { get; set; }
        [Required, MaxLength(50)]
        public string DesignationName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? DepartmentId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;
    }

    public class Skill
    {
        [Key]
        public long SkillId { get; set; }
        [Required, MaxLength(50)]
        public string SkillName { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Category { get; set; } = string.Empty;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;
    }


    public class EmployeeSkill
    {
        [Key]
        public long EmployeeSkillId { get; set; }
        public long EmployeeId { get; set; }
        public long SkillId { get; set; }
        [MaxLength(50)]
        public string ProficiencyLevel { get; set; }
        public bool Certificate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }
}
