using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace Loans.Core.Interfaces
{
    public interface ILoanTypeService
    {
        Task<ApiResult<LoanTypeDto>> GetAllAsync();
        Task<ApiResult<LoanTypeDto>> GetByIdAsync(long id);
        Task<ApiResult<LoanTypeDto>> CreateAsync(CreateLoanTypeDto dto);
        Task<ApiResult<LoanTypeDto>> UpdateAsync(UpdateLoanTypeDto dto);
        Task<ApiResult<bool>> DeleteAsync(long id, long modifiedBy);
    }
}
