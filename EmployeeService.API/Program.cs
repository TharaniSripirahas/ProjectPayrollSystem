using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Helpers;
using Payroll.Common.Configurations;
using EmployeeService.Core.Interfaces;
using EmployeeService.Infrastructure.Services;
using AdminService.Core.Interfaces;
using AdminService.Infrastructure.Services;
using Payroll.Common;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- DATABASE --------------------
builder.Services.AddDbContext<DbContextPayrollProject>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("UserDatabase"),
        b => b.MigrationsAssembly("Payroll.Common")
    )
);


// -------------------- JWT SETTINGS --------------------
// Bind Jwt section to JwtSettings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);

// -------------------- HELPERS --------------------
builder.Services.AddSingleton<IEncryptionHelper, EncryptionHelper>();

builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("Security"));
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();

// -------------------- DEPENDENCY INJECTION --------------------
builder.Services.AddScoped<IEmployeeService, EmployeeService.Infrastructure.Services.EmployeeService>();
builder.Services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDesignationService, DesignationRepository>();
builder.Services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
builder.Services.AddScoped<ISkillService, SkillService>();

// -------------------- AUTH & AUTHORIZATION --------------------
builder.Services.AddAuthorization();
// If you plan to use JWT authentication, add this:


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
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});


// -------------------- BUILD APP --------------------
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
