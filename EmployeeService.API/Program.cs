using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Payroll.Common.Helpers;
using Payroll.Common.Configurations;
using EmployeeService.Core.Interfaces;
using EmployeeService.Infrastructure.Services;
using AdminService.Core.Interfaces;
using AdminService.Infrastructure.Services;
using Payroll.Common.DatabaseContext;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

//  SWAGGER 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  DATABASE 
builder.Services.AddDbContext<PayrollDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UserDatabase"))
);

// JWT SETTINGS
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// HELPERS 
builder.Services.AddSingleton<IEncryptionHelper, EncryptionHelper>();
builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("Security"));
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();

// DEPENDENCY INJECTION 

builder.Services.AddScoped<IEmployeeService, EmployeeService.Infrastructure.Services.EmployeeService>();
builder.Services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDesignationService, DesignationRepository>(); // fixed
builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
builder.Services.AddScoped<ISkillService, SkillService>();

// Admin services
// (Add your AdminService DI here as needed)

// AUTHENTICATION & AUTHORIZATION 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidAudience = jwtSettings?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings?.Key ?? string.Empty)
        )
    };
});

builder.Services.AddAuthorization();

//  BUILD APP 
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
