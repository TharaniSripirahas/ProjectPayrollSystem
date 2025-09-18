using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Infrastructure.Services
{
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly PayrollDbContext _context;

        public LoanRepaymentService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanRepaymentDto>> GetAllAsync()
        {
            return await _context.LoanRepayments
                .Include(r => r.Loan)
                .ThenInclude(l => l.Employee) // Assuming Employee is a navigation property in EmployeeLoan
                .Include(r => r.Loan)
                .ThenInclude(l => l.LoanType) // Assuming LoanType is a navigation property in EmployeeLoan
                .Include(r => r.PayrollCycle)
                .Select(r => new LoanRepaymentDto
                {
                    RepaymentId = r.RepaymentId,
                    LoanId = r.LoanId,
                    EmployeeName = r.Loan.Employee.FirstName + " " + r.Loan.Employee.LastName,
                    LoanTypeName = r.Loan.LoanType.TypeName,
                    PaymentDate = r.PaymentDate,
                    PayrollCycleId = r.PayrollCycleId,
                    PayrollCycleName = r.PayrollCycle.PayrollCycleName,  // Map payroll cycle name
                    Amount = r.Amount,
                    InterestAmount = r.InterestAmount,
                    RemainingBalance = r.RemainingBalance,
                    RecordStatus = r.RecordStatus
                })
                .ToListAsync();
        }

        public async Task<LoanRepaymentDto?> GetByIdAsync(long repaymentId)
        {
            return await _context.LoanRepayments
                .Include(r => r.Loan)
                .ThenInclude(l => l.Employee)
                .Include(r => r.Loan)
                .ThenInclude(l => l.LoanType)
                .Include(r => r.PayrollCycle)
                .Where(r => r.RepaymentId == repaymentId)
                .Select(r => new LoanRepaymentDto
                {
                    RepaymentId = r.RepaymentId,
                    LoanId = r.LoanId,
                    EmployeeName = r.Loan.Employee.FirstName + " " + r.Loan.Employee.LastName,
                    LoanTypeName = r.Loan.LoanType.TypeName,
                    PaymentDate = r.PaymentDate,
                    PayrollCycleId = r.PayrollCycleId,
                    PayrollCycleName = r.PayrollCycle.PayrollCycleName,
                    Amount = r.Amount,
                    InterestAmount = r.InterestAmount,
                    RemainingBalance = r.RemainingBalance,
                    RecordStatus = r.RecordStatus
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LoanRepaymentDto> CreateAsync(LoanRepaymentCreateDto dto)
        {
            var repayment = new LoanRepayment
            {
                LoanId = dto.LoanId,
                PaymentDate = dto.PaymentDate,
                PayrollCycleId = dto.PayrollCycleId,
                Amount = dto.Amount,
                InterestAmount = dto.InterestAmount,
                RemainingBalance = dto.RemainingBalance,
                CreatedBy = dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.LoanRepayments.Add(repayment);
            await _context.SaveChangesAsync();

            // Fetch related data for response
            var createdRepayment = await GetByIdAsync(repayment.RepaymentId);
            return createdRepayment!;
        }

        public async Task<LoanRepaymentDto?> UpdateAsync(LoanRepaymentUpdateDto dto)
        {
            var repayment = await _context.LoanRepayments.FindAsync(dto.RepaymentId);
            if (repayment == null) return null;

            repayment.Amount = dto.Amount;
            repayment.InterestAmount = dto.InterestAmount;
            repayment.RemainingBalance = dto.RemainingBalance;
            repayment.LastModifiedBy = dto.LastModifiedBy;
            repayment.LastModifiedOn = DateTime.UtcNow;

            _context.LoanRepayments.Update(repayment);
            await _context.SaveChangesAsync();

            var updatedRepayment = await GetByIdAsync(repayment.RepaymentId);
            return updatedRepayment;
        }

        public async Task<bool> DeleteAsync(long repaymentId)
        {
            var repayment = await _context.LoanRepayments.FindAsync(repaymentId);
            if (repayment == null) return false;

            _context.LoanRepayments.Remove(repayment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
