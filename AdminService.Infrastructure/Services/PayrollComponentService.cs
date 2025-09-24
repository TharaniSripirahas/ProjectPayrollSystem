using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class PayrollComponentService : IPayrollComponentService
    {
        private readonly PayrollDbContext _context;

        public PayrollComponentService(PayrollDbContext context)
        {
            _context = context;
        }

        // Get all payroll components
        public async Task<IEnumerable<PayrollComponentDto>> GetAllAsync()
        {
            return await _context.PayrollComponents
                .Include(c => c.Record)
                    .ThenInclude(r => r.PayrollCycle)
                .Include(c => c.Record)
                    .ThenInclude(r => r.Employee)
                .Include(c => c.Component) 
                .Select(c => new PayrollComponentDto
                {
                    PayrollComponentId = c.PayrollComponentId,
                    RecordId = c.RecordId,
                    ComponentId = c.ComponentId,
                    Amount = c.Amount,
                    IsEarning = c.IsEarning,
                    RecordStatus = c.RecordStatus,
                    PayrollCycleName = c.Record.PayrollCycle != null ? c.Record.PayrollCycle.PayrollCycleName : null,
                    EmployeeName = c.Record.Employee != null ? c.Record.Employee.FirstName + " " + c.Record.Employee.LastName : null,
                    ComponentName = c.Component != null ? c.Component.ComponentName : null

                })
                .ToListAsync();
        }

        // Get a payroll component by Id
        public async Task<PayrollComponentDto?> GetByIdAsync(long payrollComponentId)
        {
            var component = await _context.PayrollComponents
                .Include(c => c.Record)
                    .ThenInclude(r => r.PayrollCycle)
                .Include(c => c.Record)
                    .ThenInclude(r => r.Employee)
                .Include(c => c.Component)
                .FirstOrDefaultAsync(c => c.PayrollComponentId == payrollComponentId);

            if (component == null) return null;

            return new PayrollComponentDto
            {
                PayrollComponentId = component.PayrollComponentId,
                RecordId = component.RecordId,
                ComponentId = component.ComponentId,
                Amount = component.Amount,
                IsEarning = component.IsEarning,
                RecordStatus = component.RecordStatus,
                PayrollCycleName = component.Record?.PayrollCycle?.PayrollCycleName,
                EmployeeName = component.Record?.Employee != null
                    ? component.Record.Employee.FirstName + " " + component.Record.Employee.LastName
                    : null,
                ComponentName = component.Component?.ComponentName
            };
        }

        // Create new payroll component
        public async Task<PayrollComponentDto> CreateAsync(CreatePayrollComponentDto dto)
        {
            var component = new PayrollComponent
            {
                RecordId = dto.RecordId,
                ComponentId = dto.ComponentId,
                Amount = dto.Amount,
                IsEarning = dto.IsEarning,
                CreatedBy = 1, 
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.PayrollComponents.Add(component);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(component.PayrollComponentId)
                   ?? throw new Exception("Error retrieving created PayrollComponent");
        }

        // Update payroll component
        public async Task<PayrollComponentDto?> UpdateAsync(long payrollComponentId, UpdatePayrollComponentDto dto)
        {
            var component = await _context.PayrollComponents.FindAsync(payrollComponentId);
            if (component == null) return null;

            component.RecordId = dto.RecordId;
            component.ComponentId = dto.ComponentId;
            component.Amount = dto.Amount;
            component.IsEarning = dto.IsEarning;
            component.RecordStatus = dto.RecordStatus;
            component.LastModifiedBy = 1; 
            component.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(component.PayrollComponentId);
        }

        // delete payroll component
        public async Task<bool> DeleteAsync(long payrollComponentId)
        {
            var component = await _context.PayrollComponents.FindAsync(payrollComponentId);
            if (component == null) return false;

            component.RecordStatus = 0; 
            component.LastModifiedBy = 1; 
            component.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
