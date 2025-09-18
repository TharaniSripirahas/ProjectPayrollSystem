using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IReimbursementClaimService
    {
        Task<IEnumerable<ReimbursementClaimDto>> GetAllAsync();
        Task<ReimbursementClaimDto?> GetByIdAsync(long claimId);
        Task<ReimbursementClaimDto> CreateAsync(CreateReimbursementClaimDto dto);
        Task<ReimbursementClaimDto?> UpdateAsync(long claimId, UpdateReimbursementClaimDto dto);
        Task<bool> DeleteAsync(long claimId);
    }
}
