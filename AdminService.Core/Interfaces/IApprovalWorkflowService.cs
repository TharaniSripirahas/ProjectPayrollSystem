using System.Collections.Generic;
using System.Threading.Tasks;
using Payroll.Common.NonEntities;  // <-- Add this
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Core.Interfaces
{
    public interface IApprovalWorkflowService
    {
        Task<IEnumerable<ApprovalWorkflowDto>> GetAllAsync();
        Task<ApprovalWorkflowDto?> GetByIdAsync(long workflowId);
        Task<ApprovalWorkflowDto> CreateAsync(CreateApprovalWorkflowDto dto);
        Task<ApprovalWorkflowDto?> UpdateAsync(long workflowId, UpdateApprovalWorkflowDto dto);
        Task<bool> DeleteAsync(long workflowId);
    }
}
