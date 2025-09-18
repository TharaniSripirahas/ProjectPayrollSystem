using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class PayrollRecordService : IPayrollRecordService
    {
        private readonly PayrollDbContext _context;

        public PayrollRecordService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayrollRecordDto>> GetAllAsync()
        {
            return await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .Select(r => new PayrollRecordDto
                {
                    RecordId = r.RecordId,
                    PayrollCycleId = r.PayrollCycleId,
                    EmployeeId = r.EmployeeId,
                    GrossEarnings = r.GrossEarnings,
                    TotalDeduction = r.TotalDeduction,
                    NetPay = r.NetPay,
                    PaymentStatus = r.PaymentStatus,
                    PayslipGenerated = r.PayslipGenerated,
                    PayslipPath = r.PayslipPath,
                    RecordStatus = r.RecordStatus,
                    PayrollCycleName = r.PayrollCycle != null ? r.PayrollCycle.PayrollCycleName : null,
                    EmployeeName = r.Employee != null ? r.Employee.FirstName + " " + r.Employee.LastName : null
                })
                .ToListAsync();
        }

        public async Task<PayrollRecordDto?> GetByIdAsync(long recordId)
        {
            var record = await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(r => r.RecordId == recordId);

            if (record == null) return null;

            return new PayrollRecordDto
            {
                RecordId = record.RecordId,
                PayrollCycleId = record.PayrollCycleId,
                EmployeeId = record.EmployeeId,
                GrossEarnings = record.GrossEarnings,
                TotalDeduction = record.TotalDeduction,
                NetPay = record.NetPay,
                PaymentStatus = record.PaymentStatus,
                PayslipGenerated = record.PayslipGenerated,
                PayslipPath = record.PayslipPath,
                RecordStatus = record.RecordStatus,
                PayrollCycleName = record.PayrollCycle?.PayrollCycleName,
                EmployeeName = record.Employee != null ? record.Employee.FirstName + " " + record.Employee.LastName : null
            };
        }

        public async Task<PayrollRecordDto> CreateAsync(CreatePayrollRecordDto dto)
        {
            var record = new PayrollRecord
            {
                PayrollCycleId = dto.PayrollCycleId,
                EmployeeId = dto.EmployeeId,
                GrossEarnings = dto.GrossEarnings,
                TotalDeduction = dto.TotalDeduction,
                NetPay = dto.NetPay,
                PaymentStatus = dto.PaymentStatus,
                PayslipGenerated = dto.PayslipGenerated,
                PayslipPath = dto.PayslipPath,
                CreatedBy = 1, // TODO: replace with logged-in user
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.PayrollRecords.Add(record);
            await _context.SaveChangesAsync();

            // Reload with navigation properties so names are included
            var savedRecord = await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .FirstAsync(r => r.RecordId == record.RecordId);

            return new PayrollRecordDto
            {
                RecordId = savedRecord.RecordId,
                PayrollCycleId = savedRecord.PayrollCycleId,
                EmployeeId = savedRecord.EmployeeId,
                GrossEarnings = savedRecord.GrossEarnings,
                TotalDeduction = savedRecord.TotalDeduction,
                NetPay = savedRecord.NetPay,
                PaymentStatus = savedRecord.PaymentStatus,
                PayslipGenerated = savedRecord.PayslipGenerated,
                PayslipPath = savedRecord.PayslipPath,
                RecordStatus = savedRecord.RecordStatus,
                PayrollCycleName = savedRecord.PayrollCycle?.PayrollCycleName,
                EmployeeName = savedRecord.Employee != null ? savedRecord.Employee.FirstName + " " + savedRecord.Employee.LastName : null
            };
        }

        public async Task<PayrollRecordDto?> UpdateAsync(long recordId, UpdatePayrollRecordDto dto)
        {
            var record = await _context.PayrollRecords.FindAsync(recordId);
            if (record == null) return null;

            record.PayrollCycleId = dto.PayrollCycleId;
            record.EmployeeId = dto.EmployeeId;
            record.GrossEarnings = dto.GrossEarnings;
            record.TotalDeduction = dto.TotalDeduction;
            record.NetPay = dto.NetPay;
            record.PaymentStatus = dto.PaymentStatus;
            record.PayslipGenerated = dto.PayslipGenerated;
            record.PayslipPath = dto.PayslipPath;
            record.RecordStatus = dto.RecordStatus;
            record.LastModifiedBy = 1; // TODO: replace with logged-in user
            record.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload with navigation properties so names are included
            var updatedRecord = await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .FirstAsync(r => r.RecordId == record.RecordId);

            return new PayrollRecordDto
            {
                RecordId = updatedRecord.RecordId,
                PayrollCycleId = updatedRecord.PayrollCycleId,
                EmployeeId = updatedRecord.EmployeeId,
                GrossEarnings = updatedRecord.GrossEarnings,
                TotalDeduction = updatedRecord.TotalDeduction,
                NetPay = updatedRecord.NetPay,
                PaymentStatus = updatedRecord.PaymentStatus,
                PayslipGenerated = updatedRecord.PayslipGenerated,
                PayslipPath = updatedRecord.PayslipPath,
                RecordStatus = updatedRecord.RecordStatus,
                PayrollCycleName = updatedRecord.PayrollCycle?.PayrollCycleName,
                EmployeeName = updatedRecord.Employee != null ? updatedRecord.Employee.FirstName + " " + updatedRecord.Employee.LastName : null
            };
        }

        public async Task<bool> DeleteAsync(long recordId)
        {
            var record = await _context.PayrollRecords.FindAsync(recordId);
            if (record == null) return false;

            _context.PayrollRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
