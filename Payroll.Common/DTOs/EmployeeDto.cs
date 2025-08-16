using Payroll.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.DTOs
{
    public class EmployeeDto
    {
        public long EmployeeId { get; set; }
        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string PhoneNumber { get; set; } = string.Empty;
        public string? Gender { get; set; }
        [Required] public DateOnly DateOfBirth { get; set; }
        [Required] public long DepartmentId { get; set; }
        [Required] public long DesignationId { get; set; }
        public string? SkillLevel { get; set; }
        public string? TechnologyTags { get; set; }
        [Required] public long EmploymentType { get; set; }
        [Required] public DateOnly JoinDate { get; set; }
        public DateOnly? ExitDate { get; set; }
        [Required] public string BankName { get; set; } = string.Empty;
        [Required] public string BankAccountNumber { get; set; } = string.Empty;
        [Required] public string IfscCode { get; set; } = string.Empty;
        [Required] public string PfNumber { get; set; } = string.Empty;
        [Required] public string EsiNumber { get; set; } = string.Empty;
        [Required] public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public RecordStatus RecordStatus { get; set; } = RecordStatus.Active;
        [Required] public string Password { get; set; } = string.Empty;
    }

    public class ApiResponse<T>
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorDesc { get; set; } = string.Empty;
        public List<T> ResponseData { get; set; } = new();
    }

    public class RolePrivilegeDto
    {
        public int PrivilegeID { get; set; }
        public string PrivilegeName { get; set; } = string.Empty;
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class DepartmentDto
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? ManagerId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }
    public class DesignationDto
    {
        public long DesignationId { get; set; }
        [Required] public string DesignationName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? DepartmentId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class CreateDesignationRequest
    {
        [Required] public string DesignationName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? DepartmentId { get; set; }
        [Required] public long CreatedBy { get; set; }
    }

    public class SkillDto
    {
        public long SkillId { get; set; }
        [Required] public string SkillName { get; set; } = string.Empty;
        [Required] public string Category { get; set; } = string.Empty;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class CreateSkillRequest
    {
        [Required] public string SkillName { get; set; } = string.Empty;
        [Required] public string Category { get; set; } = string.Empty;
        [Required] public long CreatedBy { get; set; }
    }

    public class EmployeeSkillDto
    {
        public long EmployeeSkillId { get; set; }
        [Required] public long EmployeeId { get; set; }
        [Required] public long SkillId { get; set; }
        public string? ProficiencyLevel { get; set; }
        public bool Certificate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class CreateEmployeeSkillRequest
    {
        [Required] public long EmployeeId { get; set; }
        [Required] public long SkillId { get; set; }
        public string? ProficiencyLevel { get; set; }
        public bool Certificate { get; set; }
        [Required] public long CreatedBy { get; set; }
    }

    public class EmployeeTypeDto
    {
        public long EmployeeTypeId { get; set; }
        [Required] public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;
    }
}
