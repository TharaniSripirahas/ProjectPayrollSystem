using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace AdminService.Core.Interfaces
{
    public interface IApprovalLevelService
    {
        Task<IEnumerable<ApprovalLevelDto>> GetAllAsync();
        Task<ApprovalLevelDto?> GetByIdAsync(long levelId);
        Task<ApprovalLevelDto> CreateAsync(CreateApprovalLevelDto dto);
        Task<ApprovalLevelDto?> UpdateAsync(long levelId, UpdateApprovalLevelDto dto);
        Task<bool> DeleteAsync(long levelId);
    }
}
