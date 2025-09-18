using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Core.Interfaces
{
    public interface IApprovalActionService
    {
        Task<IEnumerable<ApprovalActionDto>> GetAllAsync();
        Task<ApprovalActionDto?> GetByIdAsync(long actionId);
        Task<ApprovalActionDto> CreateAsync(CreateApprovalActionDto dto);
        Task<ApprovalActionDto?> UpdateAsync(long actionId, UpdateApprovalActionDto dto);
        Task<bool> DeleteAsync(long actionId);
    }
}
