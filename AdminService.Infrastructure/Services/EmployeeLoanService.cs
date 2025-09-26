using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Infrastructure.Services
{
    public class EmployeeLoanService : IEmployeeLoanService
    {
        private readonly PayrollDbContext _context;
        private readonly ILogger<EmployeeLoanService> _logger;

        public EmployeeLoanService(PayrollDbContext context, ILogger<EmployeeLoanService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResult<EmployeeLoanDto>> GetAllAsync()
        {
            var response = new ApiResult<EmployeeLoanDto>();
            try
            {
                var loans = await _context.EmployeeLoans
                    .Include(l => l.Employee)
                    .Include(l => l.LoanType)
                    .Where(l => l.RecordStatus == 0)
                    .Select(l => new EmployeeLoanDto
                    {
                        LoanId = l.LoanId,
                        EmployeeId = l.EmployeeId,
                        EmployeeName = l.Employee != null ? l.Employee.FirstName + " " + l.Employee.LastName : null,
                        LoanTypeId = l.LoanTypeId,
                        LoanTypeName = l.LoanType != null ? l.LoanType.TypeName : null,
                        Amount = l.Amount,
                        SanctionDate = l.SanctionDate,
                        TenureMonths = l.TenureMonths,
                        InterestRate = l.InterestRate,
                        EmiAmount = l.EmiAmount,
                        Status = l.Status,
                        Purpose = l.Purpose
                    })
                    .ToListAsync();

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.AddRange(loans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee loans");
                response.ResponseCode = 0;
                response.Message = "Error";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<EmployeeLoanDto>> GetByIdAsync(long loanId)
        {
            var response = new ApiResult<EmployeeLoanDto>();
            try
            {
                var loan = await _context.EmployeeLoans
                    .Include(l => l.Employee)
                    .Include(l => l.LoanType)
                    .FirstOrDefaultAsync(l => l.LoanId == loanId && l.RecordStatus == 0);

                if (loan == null)
                {
                    response.ResponseCode = 2;
                    response.Message = "Loan not found";
                    return response;
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(new EmployeeLoanDto
                {
                    LoanId = loan.LoanId,
                    EmployeeId = loan.EmployeeId,
                    LoanTypeId = loan.LoanTypeId,
                    Amount = loan.Amount,
                    SanctionDate = loan.SanctionDate,
                    TenureMonths = loan.TenureMonths,
                    InterestRate = loan.InterestRate,
                    EmiAmount = loan.EmiAmount,
                    Status = loan.Status,
                    Purpose = loan.Purpose,
                    EmployeeName = loan.Employee != null ? loan.Employee.FirstName + " " + loan.Employee.LastName : null,
                    LoanTypeName = loan.LoanType?.TypeName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee loan by id");
                response.ResponseCode = 0;
                response.Message = "Error";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<EmployeeLoanDto>> CreateAsync(CreateEmployeeLoanDto dto)
        {
            var response = new ApiResult<EmployeeLoanDto>();
            try
            {
                var loan = new EmployeeLoan
                {
                    EmployeeId = dto.EmployeeId,
                    LoanTypeId = dto.LoanTypeId,
                    Amount = dto.Amount,
                    SanctionDate = dto.SanctionDate,
                    TenureMonths = dto.TenureMonths,
                    InterestRate = dto.InterestRate,
                    EmiAmount = dto.EmiAmount,
                    Status = dto.Status,
                    Purpose = dto.Purpose,
                    CreatedBy = dto.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    RecordStatus = 0
                };

                _context.EmployeeLoans.Add(loan);
                await _context.SaveChangesAsync();

                await _context.Entry(loan).Reference(l => l.Employee).LoadAsync();
                await _context.Entry(loan).Reference(l => l.LoanType).LoadAsync();

                response.ResponseCode = 1;
                response.Message = "Created successfully";
                response.ResponseData.Add(new EmployeeLoanDto
                {
                    LoanId = loan.LoanId,
                    EmployeeId = loan.EmployeeId,
                    LoanTypeId = loan.LoanTypeId,
                    Amount = loan.Amount,
                    SanctionDate = loan.SanctionDate,
                    TenureMonths = loan.TenureMonths,
                    InterestRate = loan.InterestRate,
                    EmiAmount = loan.EmiAmount,
                    Status = loan.Status,
                    Purpose = loan.Purpose,
                    EmployeeName = loan.Employee != null ? loan.Employee.FirstName + " " + loan.Employee.LastName : null,
                    LoanTypeName = loan.LoanType?.TypeName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee loan");
                response.ResponseCode = 0;
                response.Message = "Error";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<EmployeeLoanDto>> UpdateAsync(UpdateEmployeeLoanDto dto)
        {
            var response = new ApiResult<EmployeeLoanDto>();
            try
            {
                var loan = await _context.EmployeeLoans.FindAsync(dto.LoanId);
                if (loan == null || loan.RecordStatus != 0)
                {
                    response.ResponseCode = 2;
                    response.Message = "Loan not found";
                    return response;
                }

                loan.EmployeeId = dto.EmployeeId; 
                loan.LoanTypeId = dto.LoanTypeId; 
                loan.Amount = dto.Amount;
                loan.TenureMonths = dto.TenureMonths;
                loan.InterestRate = dto.InterestRate;
                loan.EmiAmount = dto.EmiAmount;
                loan.Status = dto.Status;
                loan.Purpose = dto.Purpose;
                loan.LastModifiedBy = dto.LastModifiedBy;
                loan.LastModifiedOn = DateTime.UtcNow;

                _context.EmployeeLoans.Update(loan);
                await _context.SaveChangesAsync();

                await _context.Entry(loan).Reference(l => l.Employee).LoadAsync();
                await _context.Entry(loan).Reference(l => l.LoanType).LoadAsync();

                response.ResponseCode = 1;
                response.Message = "Updated successfully";
                response.ResponseData.Add(new EmployeeLoanDto
                {
                    LoanId = loan.LoanId,
                    EmployeeId = loan.EmployeeId,
                    LoanTypeId = loan.LoanTypeId,
                    Amount = loan.Amount,
                    SanctionDate = loan.SanctionDate,
                    TenureMonths = loan.TenureMonths,
                    InterestRate = loan.InterestRate,
                    EmiAmount = loan.EmiAmount,
                    Status = loan.Status,
                    Purpose = loan.Purpose,
                    EmployeeName = loan.Employee != null ? loan.Employee.FirstName + " " + loan.Employee.LastName : null,
                    LoanTypeName = loan.LoanType?.TypeName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee loan");
                response.ResponseCode = 0;
                response.Message = "Error";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<bool>> DeleteAsync(long loanId, long modifiedBy)
        {
            var response = new ApiResult<bool>();
            try
            {
                var loan = await _context.EmployeeLoans.FindAsync(loanId);
                if (loan == null || loan.RecordStatus != 0)
                {
                    response.ResponseCode = 2;
                    response.Message = "Loan not found";
                    return response;
                }

                loan.RecordStatus = 1;
                loan.LastModifiedBy = modifiedBy;
                loan.LastModifiedOn = DateTime.UtcNow;

                _context.EmployeeLoans.Update(loan);
                await _context.SaveChangesAsync();

                response.ResponseCode = 1;
                response.Message = "Deleted successfully";
                response.ResponseData.Add(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee loan");
                response.ResponseCode = 0;
                response.Message = "Error";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }
    }
}
