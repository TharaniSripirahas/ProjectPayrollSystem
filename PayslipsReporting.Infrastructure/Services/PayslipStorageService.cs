using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using PayslipsReporting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipsReporting.Infrastructure.Services
{
    public class PayslipStorageService : IPayslipStorageService
    {
        private readonly PayrollDbContext _context;

        public PayslipStorageService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayslipsReportingDto.PayslipDto>> GetAllAsync()
        {
            return await _context.PayslipStorages
                .Include(p => p.Employee)
                .Include(p => p.Payroll)
                    .ThenInclude(pr => pr.PayrollCycle)
                .Select(p => new PayslipsReportingDto.PayslipDto
                {
                    PayslipId = p.PayslipId,
                    PayrollId = p.PayrollId,
                    PayrollName = p.Payroll != null && p.Payroll.PayrollCycle != null
                    ? p.Payroll.PayrollCycle.PayrollCycleName
                    : null,
                    EmployeeId = p.EmployeeId,
                    EmployeeName = p.Employee != null
                    ? p.Employee.FirstName + " " + p.Employee.LastName
                    : null,
                    FilePath = p.FilePath,
                    FileHash = p.FileHash,
                    GeneratedAt = p.GeneratedAt,
                    IsDelivered = p.IsDelivered,
                    DeliveryMethod = p.DeliveryMethod,
                    DeliveryDate = p.DeliveryDate,
                    TdsSheetPath = p.TdsSheetPath,
                    RecordStatus = p.RecordStatus
                })
                .ToListAsync();
        }


        public async Task<PayslipsReportingDto.PayslipDto?> GetByIdAsync(long payslipId)
        {
            var payslip = await _context.PayslipStorages
                .Include(p => p.Employee)
                .Include(p => p.Payroll)
                .FirstOrDefaultAsync(p => p.PayslipId == payslipId);

            if (payslip == null) return null;

            return new PayslipsReportingDto.PayslipDto
            {
                PayslipId = payslip.PayslipId,
                PayrollId = payslip.PayrollId,
                PayrollName = payslip.Payroll != null ? payslip.Payroll.PayrollCycle.PayrollCycleName : null,
                EmployeeId = payslip.EmployeeId,
                EmployeeName = payslip.Employee != null
                ? payslip.Employee.FirstName + " " + payslip.Employee.LastName
                : null,
                FilePath = payslip.FilePath,
                FileHash = payslip.FileHash,
                GeneratedAt = payslip.GeneratedAt,
                IsDelivered = payslip.IsDelivered,
                DeliveryMethod = payslip.DeliveryMethod,
                DeliveryDate = payslip.DeliveryDate,
                TdsSheetPath = payslip.TdsSheetPath,
                RecordStatus = payslip.RecordStatus
            };
        }


        public async Task<PayslipsReportingDto.PayslipDto> CreateAsync(PayslipsReportingDto.CreatePayslipDto dto)
        {
            var employee = await _context.Employees.FindAsync(dto.EmployeeId);
            if (employee == null)
                throw new Exception($"Employee with ID {dto.EmployeeId} not found.");

            var payroll = await _context.PayrollRecords
                .Include(pr => pr.PayrollCycle)
                .FirstOrDefaultAsync(pr => pr.RecordId == dto.PayrollId); 

            if (payroll == null)
                throw new Exception($"Payroll record with ID {dto.PayrollId} not found.");

            var entity = new PayslipStorage
            {
                PayrollId = dto.PayrollId,
                EmployeeId = dto.EmployeeId,
                FilePath = dto.FilePath,
                FileHash = dto.FileHash,
                GeneratedAt = DateTime.UtcNow,
                IsDelivered = dto.IsDelivered,
                DeliveryMethod = dto.DeliveryMethod,
                DeliveryDate = dto.DeliveryDate,
                TdsSheetPath = dto.TdsSheetPath,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1,
                Employee = employee,
                Payroll = payroll
            };

            _context.PayslipStorages.Add(entity);
            await _context.SaveChangesAsync();

            return new PayslipsReportingDto.PayslipDto
            {
                PayslipId = entity.PayslipId,
                PayrollId = entity.PayrollId,
                PayrollName = payroll.PayrollCycle?.PayrollCycleName, 
                EmployeeId = entity.EmployeeId,
                EmployeeName = employee.FirstName + " " + employee.LastName,
                FilePath = entity.FilePath,
                FileHash = entity.FileHash,
                GeneratedAt = entity.GeneratedAt,
                IsDelivered = entity.IsDelivered,
                DeliveryMethod = entity.DeliveryMethod,
                DeliveryDate = entity.DeliveryDate,
                TdsSheetPath = entity.TdsSheetPath,
                RecordStatus = entity.RecordStatus
            };
        }


        public async Task<PayslipsReportingDto.PayslipDto?> UpdateAsync(long payslipId, PayslipsReportingDto.UpdatePayslipDto dto)
        {
            var entity = await _context.PayslipStorages.FindAsync(payslipId);
            if (entity == null) return null;

            entity.PayrollId = dto.PayrollId;
            entity.EmployeeId = dto.EmployeeId;
            entity.FilePath = dto.FilePath;
            entity.FileHash = dto.FileHash;
            entity.IsDelivered = dto.IsDelivered;
            entity.DeliveryMethod = dto.DeliveryMethod;
            entity.DeliveryDate = dto.DeliveryDate;
            entity.TdsSheetPath = dto.TdsSheetPath;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = 1; 
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new PayslipsReportingDto.PayslipDto
            {
                PayslipId = entity.PayslipId,
                PayrollId = entity.PayrollId,
                EmployeeId = entity.EmployeeId,
                FilePath = entity.FilePath,
                FileHash = entity.FileHash,
                GeneratedAt = entity.GeneratedAt,
                IsDelivered = entity.IsDelivered,
                DeliveryMethod = entity.DeliveryMethod,
                DeliveryDate = entity.DeliveryDate,
                TdsSheetPath = entity.TdsSheetPath,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long payslipId)
        {
            var entity = await _context.PayslipStorages.FindAsync(payslipId);
            if (entity == null) return false;

            _context.PayslipStorages.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
