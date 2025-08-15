using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
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
}
