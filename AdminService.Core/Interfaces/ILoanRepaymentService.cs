using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Core.Interfaces
{
    public interface ILoanRepaymentService
    {
        Task<IEnumerable<LoanRepaymentDto>> GetAllAsync();
        Task<LoanRepaymentDto?> GetByIdAsync(long repaymentId);
        Task<LoanRepaymentDto> CreateAsync(LoanRepaymentCreateDto dto);
        Task<LoanRepaymentDto?> UpdateAsync(LoanRepaymentUpdateDto dto);
        Task<bool> DeleteAsync(long repaymentId);
    }
}
