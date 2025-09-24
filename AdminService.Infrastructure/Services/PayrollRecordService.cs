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
            var r = await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(r => r.RecordId == recordId);

            if (r == null) return null;

            return new PayrollRecordDto
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
            };
        }

        public async Task<PayrollRecordDto> CreateAsync(CreatePayrollRecordDto dto)
        {
            var r = new PayrollRecord
            {
                PayrollCycleId = dto.PayrollCycleId,
                EmployeeId = dto.EmployeeId,
                GrossEarnings = dto.GrossEarnings,
                TotalDeduction = dto.TotalDeduction,
                NetPay = dto.NetPay,
                PaymentStatus = dto.PaymentStatus,
                PayslipGenerated = dto.PayslipGenerated,
                PayslipPath = dto.PayslipPath,
                CreatedBy = 1, 
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.PayrollRecords.Add(r);
            await _context.SaveChangesAsync();

            var savedRecord = await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .FirstAsync(r => r.RecordId == r.RecordId);

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
                PayrollCycleName = savedRecord.PayrollCycle != null ? savedRecord.PayrollCycle.PayrollCycleName : null,
                EmployeeName = savedRecord.Employee != null ? savedRecord.Employee.FirstName + " " + savedRecord.Employee.LastName : null
            };
        }

        public async Task<PayrollRecordDto?> UpdateAsync(long recordId, UpdatePayrollRecordDto dto)
        {
            var r = await _context.PayrollRecords.FindAsync(recordId);
            if (r == null) return null;

            r.PayrollCycleId = dto.PayrollCycleId;
            r.EmployeeId = dto.EmployeeId;
            r.GrossEarnings = dto.GrossEarnings;
            r.TotalDeduction = dto.TotalDeduction;
            r.NetPay = dto.NetPay;
            r.PaymentStatus = dto.PaymentStatus;
            r.PayslipGenerated = dto.PayslipGenerated;
            r.PayslipPath = dto.PayslipPath;
            r.RecordStatus = dto.RecordStatus;
            r.LastModifiedBy = 1; 
            r.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedRecord = await _context.PayrollRecords
                .Include(r => r.PayrollCycle)
                .Include(r => r.Employee)
                .FirstAsync(r => r.RecordId == r.RecordId);

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
                PayrollCycleName = updatedRecord.PayrollCycle != null ? updatedRecord.PayrollCycle.PayrollCycleName : null,
                EmployeeName = updatedRecord.Employee != null ? updatedRecord.Employee.FirstName + " " + updatedRecord.Employee.LastName : null
            };
        }

        public async Task<bool> DeleteAsync(long recordId)
        {
            var r = await _context.PayrollRecords.FindAsync(recordId);
            if (r == null) return false;

            _context.PayrollRecords.Remove(r);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
