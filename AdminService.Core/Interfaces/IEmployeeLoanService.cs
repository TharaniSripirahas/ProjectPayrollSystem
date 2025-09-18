using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Core.Interfaces
{
    public interface IEmployeeLoanService
    {
        Task<ApiResult<EmployeeLoanDto>> GetAllAsync();
        Task<ApiResult<EmployeeLoanDto>> GetByIdAsync(long loanId);
        Task<ApiResult<EmployeeLoanDto>> CreateAsync(CreateEmployeeLoanDto dto);
        Task<ApiResult<EmployeeLoanDto>> UpdateAsync(UpdateEmployeeLoanDto dto);
        Task<ApiResult<bool>> DeleteAsync(long loanId, long modifiedBy);
    }
}
