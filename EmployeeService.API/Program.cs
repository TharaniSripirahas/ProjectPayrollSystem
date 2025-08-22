using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Payroll.Common.DatabaseContext;
using System.Text;
using EmployeeService.Core.Interfaces;
using EmployeeService.Infrastructure.Services;
using Payroll.Common;
using Payroll.Common.Converters;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Database
builder.Services.AddDbContext<DbContextPayrollProject>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("UserDatabase"),
        b => b.MigrationsAssembly("Payroll.Common")

    )
);

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new Payroll.Common.Converters.DateTimeConverter());
//    });
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });


// Dependency Injection
builder.Services.AddScoped<IEmployeeService, EmployeeService.Infrastructure.Services.EmployeeService>();
builder.Services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDesignationService, DesignationRepository>();
builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
builder.Services.AddScoped<ISkillService, SkillService>();


builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
