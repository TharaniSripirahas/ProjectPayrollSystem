using Loans.Core.Interfaces;
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

namespace Loans.Infrastructure.Service
{
    public class LoanTypeService : ILoanTypeService
    {
        private readonly PayrollDbContext _context;
        private readonly ILogger<LoanTypeService> _logger;

        public LoanTypeService(PayrollDbContext context, ILogger<LoanTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResult<LoanTypeDto>> GetAllAsync()
        {
            var response = new ApiResult<LoanTypeDto>();
            try
            {
                var loanTypes = await _context.LoanTypes
                    .Where(x => x.RecordStatus == 0)
                    .Select(x => new LoanTypeDto
                    {
                        LoanTypeId = x.LoanTypeId,
                        TypeName = x.TypeName,
                        MaxAmount = x.MaxAmount,
                        MaxTenureMonths = x.MaxTenureMonths,
                        InterestRate = x.InterestRate
                    }).ToListAsync();

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.AddRange(loanTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan types");
                response.ResponseCode = 0;
                response.Message = "Failed";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<LoanTypeDto>> GetByIdAsync(long id)
        {
            var response = new ApiResult<LoanTypeDto>();
            try
            {
                var loanType = await _context.LoanTypes.FindAsync(id);
                if (loanType == null || loanType.RecordStatus != 0)
                {
                    response.ResponseCode = 2;
                    response.Message = "Not found";
                    return response;
                }

                response.ResponseCode = 1;
                response.Message = "Success";
                response.ResponseData.Add(new LoanTypeDto
                {
                    LoanTypeId = loanType.LoanTypeId,
                    TypeName = loanType.TypeName,
                    MaxAmount = loanType.MaxAmount,
                    MaxTenureMonths = loanType.MaxTenureMonths,
                    InterestRate = loanType.InterestRate
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching loan type by id");
                response.ResponseCode = 0;
                response.Message = "Failed";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<LoanTypeDto>> CreateAsync(CreateLoanTypeDto dto)
        {
            var response = new ApiResult<LoanTypeDto>();
            try
            {
                var loanType = new LoanType
                {
                    TypeName = dto.TypeName,
                    MaxAmount = dto.MaxAmount,
                    MaxTenureMonths = dto.MaxTenureMonths,
                    InterestRate = dto.InterestRate,
                    CreatedBy = dto.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    RecordStatus = 0
                };

                _context.LoanTypes.Add(loanType);
                await _context.SaveChangesAsync();

                response.ResponseCode = 1;
                response.Message = "Created successfully";
                response.ResponseData.Add(new LoanTypeDto
                {
                    LoanTypeId = loanType.LoanTypeId,
                    TypeName = loanType.TypeName,
                    MaxAmount = loanType.MaxAmount,
                    MaxTenureMonths = loanType.MaxTenureMonths,
                    InterestRate = loanType.InterestRate
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating loan type");
                response.ResponseCode = 0;
                response.Message = "Failed";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<LoanTypeDto>> UpdateAsync(UpdateLoanTypeDto dto)
        {
            var response = new ApiResult<LoanTypeDto>();
            try
            {
                var loanType = await _context.LoanTypes.FindAsync(dto.LoanTypeId);
                if (loanType == null || loanType.RecordStatus != 0)
                {
                    response.ResponseCode = 2;
                    response.Message = "Not found";
                    return response;
                }

                loanType.TypeName = dto.TypeName;
                loanType.MaxAmount = dto.MaxAmount;
                loanType.MaxTenureMonths = dto.MaxTenureMonths;
                loanType.InterestRate = dto.InterestRate;
                loanType.LastModifiedBy = dto.LastModifiedBy;
                loanType.LastModifiedOn = DateTime.UtcNow;

                _context.LoanTypes.Update(loanType);
                await _context.SaveChangesAsync();

                response.ResponseCode = 1;
                response.Message = "Updated successfully";
                response.ResponseData.Add(new LoanTypeDto
                {
                    LoanTypeId = loanType.LoanTypeId,
                    TypeName = loanType.TypeName,
                    MaxAmount = loanType.MaxAmount,
                    MaxTenureMonths = loanType.MaxTenureMonths,
                    InterestRate = loanType.InterestRate
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating loan type");
                response.ResponseCode = 0;
                response.Message = "Failed";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }

        public async Task<ApiResult<bool>> DeleteAsync(long id, long modifiedBy)
        {
            var response = new ApiResult<bool>();
            try
            {
                var loanType = await _context.LoanTypes.FindAsync(id);
                if (loanType == null || loanType.RecordStatus != 0)
                {
                    response.ResponseCode = 2;
                    response.Message = "Not found";
                    return response;
                }

                loanType.RecordStatus = 1;
                loanType.LastModifiedBy = modifiedBy;
                loanType.LastModifiedOn = DateTime.UtcNow;

                _context.LoanTypes.Update(loanType);
                await _context.SaveChangesAsync();

                response.ResponseCode = 1;
                response.Message = "Deleted successfully";
                response.ResponseData.Add(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting loan type");
                response.ResponseCode = 0;
                response.Message = "Failed";
                response.ErrorDesc = ex.Message;
            }
            return response;
        }
    }
}
