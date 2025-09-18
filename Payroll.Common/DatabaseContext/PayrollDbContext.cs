using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.Models;

namespace Payroll.Common.DatabaseContext;

public partial class PayrollDbContext : DbContext
{
    public PayrollDbContext()
    {
    }

    public PayrollDbContext(DbContextOptions<PayrollDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApprovalAction> ApprovalActions { get; set; }

    public virtual DbSet<ApprovalLevel> ApprovalLevels { get; set; }

    public virtual DbSet<ApprovalRequest> ApprovalRequests { get; set; }

    public virtual DbSet<ApprovalWorkflow> ApprovalWorkflows { get; set; }

    public virtual DbSet<AttendanceLog> AttendanceLogs { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<DonationFund> DonationFunds { get; set; }

    public virtual DbSet<EmpSalaryStructure> EmpSalaryStructures { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeDonation> EmployeeDonations { get; set; }

    public virtual DbSet<EmployeeLoan> EmployeeLoans { get; set; }

    public virtual DbSet<EmployeeProjectMapping> EmployeeProjectMappings { get; set; }

    public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }

    public virtual DbSet<EmployeeStatutoryDetail> EmployeeStatutoryDetails { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<Form16> Form16s { get; set; }

    public virtual DbSet<GeneratedReport> GeneratedReports { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<LoanRepayment> LoanRepayments { get; set; }

    public virtual DbSet<LoanType> LoanTypes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PayrollComponent> PayrollComponents { get; set; }

    public virtual DbSet<PayrollCycle> PayrollCycles { get; set; }

    public virtual DbSet<PayrollRecord> PayrollRecords { get; set; }

    public virtual DbSet<PayslipAccessLog> PayslipAccessLogs { get; set; }

    public virtual DbSet<PayslipNotification> PayslipNotifications { get; set; }

    public virtual DbSet<PayslipStorage> PayslipStorages { get; set; }

    public virtual DbSet<PerformanceMetric> PerformanceMetrics { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectPerformance> ProjectPerformances { get; set; }

    public virtual DbSet<ReimbursementClaim> ReimbursementClaims { get; set; }

    public virtual DbSet<ReimbursementType> ReimbursementTypes { get; set; }

    public virtual DbSet<SalaryComponent> SalaryComponents { get; set; }

    public virtual DbSet<SalaryTemplate> SalaryTemplates { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<StatutoryDeduction> StatutoryDeductions { get; set; }

    public virtual DbSet<TaxDeclaration> TaxDeclarations { get; set; }

    public virtual DbSet<TemplateComponent> TemplateComponents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=genlkhrms;Username=postgres;Password=tharani@01");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApprovalAction>(entity =>
        {
            entity.HasKey(e => e.ActionId);

            entity.HasIndex(e => e.LevelId, "IX_ApprovalActions_LevelId");

            entity.HasIndex(e => e.RequestId, "IX_ApprovalActions_RequestId");

            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.Comments).HasMaxLength(500);

            entity.HasOne(d => d.Level).WithMany(p => p.ApprovalActions).HasForeignKey(d => d.LevelId);

            entity.HasOne(d => d.Request).WithMany(p => p.ApprovalActions).HasForeignKey(d => d.RequestId);
        });

        modelBuilder.Entity<ApprovalLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId);

            entity.HasIndex(e => e.WorkflowId, "IX_ApprovalLevels_WorkflowId");

            entity.HasOne(d => d.Workflow).WithMany(p => p.ApprovalLevels).HasForeignKey(d => d.WorkflowId);
        });

        modelBuilder.Entity<ApprovalRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.HasIndex(e => e.CurrentLevelId, "IX_ApprovalRequests_CurrentLevelId");

            entity.HasIndex(e => e.WorkflowId, "IX_ApprovalRequests_WorkflowId");

            entity.HasOne(d => d.CurrentLevel).WithMany(p => p.ApprovalRequests).HasForeignKey(d => d.CurrentLevelId);

            entity.HasOne(d => d.Workflow).WithMany(p => p.ApprovalRequests).HasForeignKey(d => d.WorkflowId);
        });

        modelBuilder.Entity<ApprovalWorkflow>(entity =>
        {
            entity.HasKey(e => e.WorkflowId);

            entity.Property(e => e.EntityType).HasMaxLength(50);
            entity.Property(e => e.WorkflowName).HasMaxLength(50);
        });

        modelBuilder.Entity<AttendanceLog>(entity =>
        {
            entity.HasKey(e => e.AttendanceId);

            entity.HasIndex(e => e.EmployeeName, "IX_AttendanceLogs_EmployeeName");

            entity.HasIndex(e => e.ShiftId, "IX_AttendanceLogs_ShiftId");

            entity.HasOne(d => d.EmployeeNameNavigation).WithMany(p => p.AttendanceLogs).HasForeignKey(d => d.EmployeeName);

            entity.HasOne(d => d.Shift).WithMany(p => p.AttendanceLogs).HasForeignKey(d => d.ShiftId);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.HasIndex(e => e.UserId, "IX_AuditLogs_UserId");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.TableName).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Designations_DepartmentId");

            entity.HasOne(d => d.Department).WithMany(p => p.Designations).HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<DonationFund>(entity =>
        {
            entity.HasKey(e => e.FundId);

            entity.ToTable("DonationFund");

            entity.Property(e => e.FundName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmpSalaryStructure>(entity =>
        {
            entity.HasKey(e => e.StructureId);

            entity.ToTable("EmpSalaryStructure");

            entity.HasIndex(e => e.EmployeeName, "IX_EmpSalaryStructure_EmployeeName");

            entity.HasIndex(e => e.SalaryTemplateTemplateId, "IX_EmpSalaryStructure_SalaryTemplateTemplateId");

            entity.HasIndex(e => e.TemplateId, "IX_EmpSalaryStructure_TemplateId");

            entity.HasOne(d => d.EmployeeNameNavigation).WithMany(p => p.EmpSalaryStructures).HasForeignKey(d => d.EmployeeName);

            entity.HasOne(d => d.SalaryTemplateTemplate).WithMany(p => p.EmpSalaryStructureSalaryTemplateTemplates).HasForeignKey(d => d.SalaryTemplateTemplateId);

            entity.HasOne(d => d.Template).WithMany(p => p.EmpSalaryStructureTemplates).HasForeignKey(d => d.TemplateId);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Employees_DepartmentId");

            entity.HasIndex(e => e.DesignationId, "IX_Employees_DesignationId");

            entity.HasIndex(e => e.EmployeeTypeId, "IX_Employees_EmployeeTypeId");

            entity.HasIndex(e => e.EmploymentType, "IX_Employees_EmploymentType");

            entity.Property(e => e.BankName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EsiNumber).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(15);
            entity.Property(e => e.IfscCode).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PfNumber).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Designation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.EmployeeEmployeeTypes).HasForeignKey(d => d.EmployeeTypeId);

            entity.HasOne(d => d.EmploymentTypeNavigation).WithMany(p => p.EmployeeEmploymentTypeNavigations)
                .HasForeignKey(d => d.EmploymentType)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<EmployeeDonation>(entity =>
        {
            entity.HasKey(e => e.DonationId);

            entity.ToTable("EmployeeDonation");

            entity.HasIndex(e => e.DonationFundFundId, "IX_EmployeeDonation_DonationFundFundId");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeDonation_EmployeeId");

            entity.HasIndex(e => e.FundId, "IX_EmployeeDonation_FundId");

            entity.HasOne(d => d.DonationFundFund).WithMany(p => p.EmployeeDonationDonationFundFunds).HasForeignKey(d => d.DonationFundFundId);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDonations).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Fund).WithMany(p => p.EmployeeDonationFunds).HasForeignKey(d => d.FundId);
        });

        modelBuilder.Entity<EmployeeLoan>(entity =>
        {
            entity.HasKey(e => e.LoanId);

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeLoans_EmployeeId");

            entity.HasIndex(e => e.LoanTypeId, "IX_EmployeeLoans_LoanTypeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeLoans).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.LoanType).WithMany(p => p.EmployeeLoans).HasForeignKey(d => d.LoanTypeId);
        });

        modelBuilder.Entity<EmployeeProjectMapping>(entity =>
        {
            entity.HasKey(e => e.MappingId);

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeProjectMappings_EmployeeId");

            entity.HasIndex(e => e.ProjectId, "IX_EmployeeProjectMappings_ProjectId");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeProjectMappings)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Project).WithMany(p => p.EmployeeProjectMappings)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<EmployeeSkill>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeSkills_EmployeeId");

            entity.HasIndex(e => e.SkillId, "IX_EmployeeSkills_SkillId");

            entity.Property(e => e.ProficiencyLevel).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSkills).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Skill).WithMany(p => p.EmployeeSkills).HasForeignKey(d => d.SkillId);
        });

        modelBuilder.Entity<EmployeeStatutoryDetail>(entity =>
        {
            entity.HasKey(e => e.DetailsId);

            entity.HasIndex(e => e.DeductionId, "IX_EmployeeStatutoryDetails_DeductionId");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeStatutoryDetails_EmployeeId");

            entity.HasOne(d => d.StatutoryDeduction).WithMany(p => p.EmployeeStatutoryDetails)
                .HasForeignKey(d => d.DeductionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeStatutoryDetails)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.ToTable("EmployeeType");
        });

        modelBuilder.Entity<Form16>(entity =>
        {
            entity.HasKey(e => e.FormId);

            entity.HasIndex(e => e.EmployeeId, "IX_Form16s_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Form16s)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GeneratedReport>(entity =>
        {
            entity.HasKey(e => e.ReportId);

            entity.HasIndex(e => e.GeneratedBy, "IX_GeneratedReports_GeneratedBy");

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.GeneratedReports)
                .HasForeignKey(d => d.GeneratedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveId);

            entity.HasIndex(e => e.EmployeeName, "IX_LeaveRequests_EmployeeName");

            entity.HasIndex(e => e.LeaveTypeId, "IX_LeaveRequests_LeaveTypeId");

            entity.HasOne(d => d.EmployeeNameNavigation).WithMany(p => p.LeaveRequests).HasForeignKey(d => d.EmployeeName);

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests).HasForeignKey(d => d.LeaveTypeId);
        });

        modelBuilder.Entity<LoanRepayment>(entity =>
        {
            entity.HasKey(e => e.RepaymentId);

            entity.HasIndex(e => e.LoanId, "IX_LoanRepayments_LoanId");

            entity.HasIndex(e => e.PayrollCycleId, "IX_LoanRepayments_PayrollCycleId");

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanRepayments).HasForeignKey(d => d.LoanId);

            entity.HasOne(d => d.PayrollCycle).WithMany(p => p.LoanRepayments).HasForeignKey(d => d.PayrollCycleId);
        });

        modelBuilder.Entity<LoanType>(entity =>
        {
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasIndex(e => e.RecipientId, "IX_Notifications_RecipientId");

            entity.HasIndex(e => e.SenderId, "IX_Notifications_SenderId");

            entity.HasOne(d => d.Recipient).WithMany(p => p.NotificationRecipients).HasForeignKey(d => d.RecipientId);

            entity.HasOne(d => d.Sender).WithMany(p => p.NotificationSenders).HasForeignKey(d => d.SenderId);
        });

        modelBuilder.Entity<PayrollComponent>(entity =>
        {
            entity.HasIndex(e => e.RecordId, "IX_PayrollComponents_RecordId");

            entity.HasOne(d => d.Record).WithMany(p => p.PayrollComponents)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PayrollRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId);

            entity.HasIndex(e => e.EmployeeId, "IX_PayrollRecords_EmployeeId");

            entity.HasIndex(e => e.PayrollCycleId, "IX_PayrollRecords_PayrollCycleId");

            entity.HasOne(d => d.Employee).WithMany(p => p.PayrollRecords).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.PayrollCycle).WithMany(p => p.PayrollRecords)
                .HasForeignKey(d => d.PayrollCycleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PayslipAccessLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.HasIndex(e => e.AccessedBy, "IX_PayslipAccessLogs_AccessedBy");

            entity.HasIndex(e => e.PayslipId, "IX_PayslipAccessLogs_PayslipId");

            entity.HasOne(d => d.AccessedByNavigation).WithMany(p => p.PayslipAccessLogs)
                .HasForeignKey(d => d.AccessedBy)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Payslip).WithMany(p => p.PayslipAccessLogs)
                .HasForeignKey(d => d.PayslipId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PayslipNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId);

            entity.HasIndex(e => e.EmployeeId, "IX_PayslipNotifications_EmployeeId");

            entity.HasIndex(e => e.PayrollCycleId, "IX_PayslipNotifications_PayrollCycleId");

            entity.HasOne(d => d.Employee).WithMany(p => p.PayslipNotifications).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.PayrollCycle).WithMany(p => p.PayslipNotifications)
                .HasForeignKey(d => d.PayrollCycleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PayslipStorage>(entity =>
        {
            entity.HasKey(e => e.PayslipId);

            entity.HasIndex(e => e.PayrollId, "IX_PayslipStorages_PayrollId");

            entity.HasOne(d => d.Payroll).WithMany(p => p.PayslipStorages)
                .HasForeignKey(d => d.PayrollId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PerformanceMetric>(entity =>
        {
            entity.HasKey(e => e.MetricId);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_Permissions_RoleId");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.Resource).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.ManagerId, "IX_Projects_ManagerId");

            entity.HasOne(d => d.Manager).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ProjectPerformance>(entity =>
        {
            entity.HasKey(e => e.PerformanceId);

            entity.HasIndex(e => e.EmployeeId, "IX_ProjectPerformances_EmployeeId");

            entity.HasIndex(e => e.MetricId, "IX_ProjectPerformances_MetricId");

            entity.HasIndex(e => e.ProjectId, "IX_ProjectPerformances_ProjectId");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectPerformances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Metric).WithMany(p => p.ProjectPerformances)
                .HasForeignKey(d => d.MetricId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectPerformances)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ReimbursementClaim>(entity =>
        {
            entity.HasKey(e => e.ClaimId);

            entity.HasIndex(e => e.EmployeeId, "IX_ReimbursementClaims_EmployeeId");

            entity.HasIndex(e => e.PayrollCycleId, "IX_ReimbursementClaims_PayrollCycleId");

            entity.HasIndex(e => e.TypeId, "IX_ReimbursementClaims_TypeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReimbursementClaims).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.PayrollCycle).WithMany(p => p.ReimbursementClaims)
                .HasForeignKey(d => d.PayrollCycleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Type).WithMany(p => p.ReimbursementClaims)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ReimbursementType>(entity =>
        {
            entity.HasKey(e => e.TypeId);
        });

        modelBuilder.Entity<SalaryComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId);

            entity.ToTable("SalaryComponent");

            entity.Property(e => e.ComponentName).HasMaxLength(50);
        });

        modelBuilder.Entity<SalaryTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId);

            entity.ToTable("SalaryTemplate");

            entity.HasIndex(e => e.EmployeeTypeId, "IX_SalaryTemplate_EmployeeTypeId");

            entity.Property(e => e.TemplateName).HasMaxLength(50);

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.SalaryTemplates).HasForeignKey(d => d.EmployeeTypeId);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.SkillName).HasMaxLength(50);
        });

        modelBuilder.Entity<StatutoryDeduction>(entity =>
        {
            entity.HasKey(e => e.DeductionId);
        });

        modelBuilder.Entity<TaxDeclaration>(entity =>
        {
            entity.HasKey(e => e.DeclarationId);

            entity.HasIndex(e => e.EmployeeId, "IX_TaxDeclarations_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.TaxDeclarations)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TemplateComponent>(entity =>
        {
            entity.ToTable("TemplateComponent");

            entity.HasIndex(e => e.ComponentId, "IX_TemplateComponent_ComponentId");

            entity.HasIndex(e => e.SalaryComponentModelComponentId, "IX_TemplateComponent_SalaryComponentModelComponentId");

            entity.HasIndex(e => e.TemplateId, "IX_TemplateComponent_TemplateId");

            entity.HasOne(d => d.Component).WithMany(p => p.TemplateComponentComponents).HasForeignKey(d => d.ComponentId);

            entity.HasOne(d => d.SalaryComponentModelComponent).WithMany(p => p.TemplateComponentSalaryComponentModelComponents)
                .HasForeignKey(d => d.SalaryComponentModelComponentId)
                .HasConstraintName("FK_TemplateComponent_SalaryComponent_SalaryComponentModelCompo~");

            entity.HasOne(d => d.Template).WithMany(p => p.TemplateComponents).HasForeignKey(d => d.TemplateId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
