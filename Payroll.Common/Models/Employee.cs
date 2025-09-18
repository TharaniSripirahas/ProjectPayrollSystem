using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public long DepartmentId { get; set; }

    public long DesignationId { get; set; }

    public long EmploymentType { get; set; }

    public string? SkillLevel { get; set; }

    public string? TechnologyTags { get; set; }

    public DateOnly JoinDate { get; set; }

    public DateOnly? ExitDate { get; set; }

    public string BankName { get; set; } = null!;

    public byte[] BankAccountNumber { get; set; } = null!;

    public string IfscCode { get; set; } = null!;

    public string PfNumber { get; set; } = null!;

    public string EsiNumber { get; set; } = null!;

    public long? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public string PasswordHash { get; set; } = null!;

    public long? EmployeeTypeId { get; set; }

    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();

    public virtual Department Department { get; set; } = null!;

    public virtual Designation Designation { get; set; } = null!;

    public virtual ICollection<EmpSalaryStructure> EmpSalaryStructures { get; set; } = new List<EmpSalaryStructure>();

    public virtual ICollection<EmployeeDonation> EmployeeDonations { get; set; } = new List<EmployeeDonation>();

    public virtual ICollection<EmployeeLoan> EmployeeLoans { get; set; } = new List<EmployeeLoan>();

    public virtual ICollection<EmployeeProjectMapping> EmployeeProjectMappings { get; set; } = new List<EmployeeProjectMapping>();

    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual ICollection<EmployeeStatutoryDetail> EmployeeStatutoryDetails { get; set; } = new List<EmployeeStatutoryDetail>();

    public virtual EmployeeType? EmployeeType { get; set; }

    public virtual EmployeeType EmploymentTypeNavigation { get; set; } = null!;

    public virtual ICollection<Form16> Form16s { get; set; } = new List<Form16>();

    public virtual ICollection<GeneratedReport> GeneratedReports { get; set; } = new List<GeneratedReport>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Notification> NotificationRecipients { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationSenders { get; set; } = new List<Notification>();

    public virtual ICollection<PayrollRecord> PayrollRecords { get; set; } = new List<PayrollRecord>();

    public virtual ICollection<PayslipAccessLog> PayslipAccessLogs { get; set; } = new List<PayslipAccessLog>();

    public virtual ICollection<PayslipNotification> PayslipNotifications { get; set; } = new List<PayslipNotification>();

    public virtual ICollection<ProjectPerformance> ProjectPerformances { get; set; } = new List<ProjectPerformance>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<ReimbursementClaim> ReimbursementClaims { get; set; } = new List<ReimbursementClaim>();

    public virtual ICollection<TaxDeclaration> TaxDeclarations { get; set; } = new List<TaxDeclaration>();
}
