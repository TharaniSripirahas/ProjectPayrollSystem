//using Microsoft.EntityFrameworkCore;
//using Payroll.Common.DatabaseContext;
//using Payroll.Common.Helpers;
//using Payroll.Common.Configurations;
//using AdminService.Core.Interfaces;
//using AdminService.Infrastructure.Services;
//using EmployeeService.Core.Interfaces;
//using AdminService.API.Controllers;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//// Database
//builder.Services.AddDbContext<PayrollDbContext>(options =>
//    options.UseNpgsql(
//        builder.Configuration.GetConnectionString("UserDatabase"),
//        b => b.MigrationsAssembly("Payroll.Common")

//    )
//);

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
//    });


//builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("Security"));
//builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();


//// Dependency Injection
//builder.Services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
//builder.Services.AddScoped<IDepartmentService, DepartmentService>();
//builder.Services.AddScoped<IDesignationService, DesignationRepository>();
//builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
//builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
//builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
//builder.Services.AddScoped<IShiftService, ShiftService>();
//builder.Services.AddScoped<ISkillService, SkillService>();
//builder.Services.AddScoped<ISalaryTemplateService, SalaryTemplateService>();
//builder.Services.AddScoped<ITemplateComponentService, TemplateComponentService>();
//builder.Services.AddScoped<IEmpSalaryStructureService, EmpSalaryStructureService>();
//builder.Services.AddScoped<IEmployeeProjectMappingService, EmployeeProjectMappingService>();
//builder.Services.AddScoped<IPerformanceMetricService, PerformanceMetricService>();
//builder.Services.AddScoped<IProjectPerformanceService, ProjectPerformanceService>();
//builder.Services.AddScoped<IReimbursementClaimService, ReimbursementClaimService>();
//builder.Services.AddScoped<IPayrollCycleService, PayrollCycleService>();
//builder.Services.AddScoped<IPayrollRecordService, PayrollRecordService>();
//builder.Services.AddScoped<IPayrollComponentService, PayrollComponentService>();
//builder.Services.AddScoped<IPayslipNotificationService, PayslipNotificationService>();
//builder.Services.AddScoped<IPayslipNotificationService, PayslipNotificationService>();
//builder.Services.AddScoped<ITaxDeclarationService, TaxDeclarationService>();
//builder.Services.AddScoped<IForm16Service, Form16Service>();
//builder.Services.AddScoped<IPayslipAccessLogService, PayslipAccessLogService>();
//builder.Services.AddScoped<IGeneratedReportService, GeneratedReportService>();
//builder.Services.AddScoped<IApprovalWorkflowService, ApprovalWorkflowService>();
//builder.Services.AddScoped<IApprovalLevelService, ApprovalLevelService>();
//builder.Services.AddScoped<IApprovalRequestService, ApprovalRequestService>();
//builder.Services.AddScoped<IApprovalActionService, ApprovalActionService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IPermissionService, PermissionService>();
//builder.Services.AddScoped<IAuditLogService, AuditLogService>();
//builder.Services.AddScoped<IEmployeeLoanService, EmployeeLoanService>();
//builder.Services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();
//builder.Services.AddScoped<IEmployeeStatutoryDetailService, EmployeeStatutoryDetailService>();
//builder.Services.AddScoped<IDonationFundService, DonationFundService>();
//builder.Services.AddScoped<IEmployeeDonationService, EmployeeDonationService>();





//builder.Services.AddAuthorization();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();


using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Helpers;
using Payroll.Common.Configurations;
using AdminService.Core.Interfaces;
using AdminService.Infrastructure.Services;
using EmployeeService.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeConverter());

    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database - Scaffolded DB, no migrations needed
builder.Services.AddDbContext<PayrollDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("UserDatabase")
    )
);

// Security / Helpers
builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("Security"));
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();

// Dependency Injection - Services
builder.Services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDesignationService, DesignationRepository>();
builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISalaryTemplateService, SalaryTemplateService>();
builder.Services.AddScoped<ITemplateComponentService, TemplateComponentService>();
builder.Services.AddScoped<IEmpSalaryStructureService, EmpSalaryStructureService>();
builder.Services.AddScoped<IEmployeeProjectMappingService, EmployeeProjectMappingService>();
builder.Services.AddScoped<IPerformanceMetricService, PerformanceMetricService>();
builder.Services.AddScoped<IProjectPerformanceService, ProjectPerformanceService>();
builder.Services.AddScoped<IReimbursementClaimService, ReimbursementClaimService>();
builder.Services.AddScoped<IPayrollCycleService, PayrollCycleService>();
builder.Services.AddScoped<IPayrollRecordService, PayrollRecordService>();
builder.Services.AddScoped<IPayrollComponentService, PayrollComponentService>();
builder.Services.AddScoped<IPayslipNotificationService, PayslipNotificationService>();
builder.Services.AddScoped<ITaxDeclarationService, TaxDeclarationService>();
builder.Services.AddScoped<IForm16Service, Form16Service>();
builder.Services.AddScoped<IPayslipAccessLogService, PayslipAccessLogService>();
builder.Services.AddScoped<IGeneratedReportService, GeneratedReportService>();
builder.Services.AddScoped<IApprovalWorkflowService, ApprovalWorkflowService>();
builder.Services.AddScoped<IApprovalLevelService, ApprovalLevelService>();
builder.Services.AddScoped<IApprovalRequestService, ApprovalRequestService>();
builder.Services.AddScoped<IApprovalActionService, ApprovalActionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IEmployeeLoanService, EmployeeLoanService>();
builder.Services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();
builder.Services.AddScoped<IEmployeeStatutoryDetailService, EmployeeStatutoryDetailService>();
builder.Services.AddScoped<IDonationFundService, DonationFundService>();
builder.Services.AddScoped<IEmployeeDonationService, EmployeeDonationService>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Uncomment / configure authentication if needed
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
