using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Payroll.Common.DatabaseContext;
using System.Text;
using Payroll.Common;
using System.ComponentModel;
using Payroll.Common.Helpers;
using Reimbursements.Core.Interfaces;
using Reimbursements.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Database
builder.Services.AddDbContext<PayrollDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("UserDatabase"),
        b => b.MigrationsAssembly("Payroll.Common")
    )
);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });


// Dependency Injection 
builder.Services.AddScoped<IReimbursementTypeService, ReimbursementTypeService>();

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
