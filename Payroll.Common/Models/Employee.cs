using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "date")]
        public DateOnly DateOfBirth { get; set; }

        // Foreign Keys
        public long DepartmentId { get; set; }
        public long DesignationId { get; set; }
        public long EmploymentType { get; set; }

        public string? SkillLevel { get; set; }
        public string? TechnologyTags { get; set; }

        [Column(TypeName = "date")]
        public DateOnly JoinDate { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? ExitDate { get; set; }

        [Required, MaxLength(50)]
        public string BankName { get; set; } = string.Empty;

        [Required, Column(TypeName = "bytea")]
        public byte[] BankAccountNumber { get; set; } = Array.Empty<byte>();


        [Required, MaxLength(15)]
        public string IfscCode { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string PfNumber { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string EsiNumber { get; set; } = string.Empty;

        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public int RecordStatus { get; set; } = 0;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public object UserRoles { get; internal set; }

        // Navigation properties
        public Department Department { get; set; } = null!;
        public Designation Designation { get; set; } = null!;
        public EmployeeType EmployeeType { get; set; } = null!;
    }




    public class EmployeeType
    {
        [Key]
        public long EmployeeTypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class Department
    {
        [Key]
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? ManagerId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;

        public ICollection<Designation> Designations { get; set; } = new List<Designation>();
        public ICollection<Employees> Employees { get; set; } = new List<Employees>();
    }

    public class Designation
    {
        [Key]
        public long DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public long DepartmentId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }

        public Department Department { get; set; } = null!;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmployeeSkillId { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        [Required]
        public long SkillId { get; set; }

        [Required, StringLength(50)]
        public string ProficiencyLevel { get; set; } = string.Empty;

        [Required]
        public int Certificate { get; set; }

        [Required]
        public long CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public int RecordStatus { get; set; } = 0;

        // Navigation Properties
        [ForeignKey("EmployeeId")]
        public virtual Employees Employee { get; set; } = null!;

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; } = null!;
    }
}