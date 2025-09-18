using Payroll.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.Enums.AppEnums;

namespace Payroll.Common.NonEntities
{
    public class EmployeeDto
    {
        public long EmployeeId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }

        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public long DesignationId { get; set; }
        public string? DesignationName { get; set; }

        public long EmployeeTypeId { get; set; }
        public string? EmployeeTypeName { get; set; }

        public string SkillLevel { get; set; } = string.Empty;
        public string TechnologyTags { get; set; } = string.Empty;
        public long EmploymentType { get; set; }

        public DateOnly JoinDate { get; set; }
        public DateOnly? ExitDate { get; set; }

        public string BankName { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string IfscCode { get; set; } = string.Empty;
        public string PfNumber { get; set; } = string.Empty;
        public string EsiNumber { get; set; } = string.Empty;

        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public RecordStatus RecordStatus { get; set; }
        public string Password { get; set; } = string.Empty;
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
        public string DesignationName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class SkillDto
    {
        public long SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }

    public class CreateSkillRequest
    {
        [Required]
        public string SkillName { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public long CreatedBy { get; set; }
    }

    public class EmployeeSkillDto
    {
        public long EmployeeSkillId { get; set; }
        public long EmployeeId { get; set; }
        public long SkillId { get; set; }
        public string ProficiencyLevel { get; set; } = string.Empty;
        public int Certificate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; }
    }


    public class EmployeeTypeDto
    {
        public long EmployeeTypeId { get; set; }
        [Required]
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int RecordStatus { get; set; } = 0;
    }
}
