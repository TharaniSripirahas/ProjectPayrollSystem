using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Core.Interfaces
{
    public interface IApprovalRequestService
    {
        Task<IEnumerable<ApprovalRequestDto>> GetAllAsync();
        Task<ApprovalRequestDto?> GetByIdAsync(long requestId);
        Task<ApprovalRequestDto> CreateAsync(CreateApprovalRequestDto dto);
        Task<ApprovalRequestDto?> UpdateAsync(long requestId, UpdateApprovalRequestDto dto);
        Task<bool> DeleteAsync(long requestId);
    }
}
