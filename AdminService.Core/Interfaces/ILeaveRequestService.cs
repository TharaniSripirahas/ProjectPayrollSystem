using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestDto>> GetAllAsync();
        Task<LeaveRequestDto?> GetByIdAsync(long id);
        Task<LeaveRequestDto> CreateAsync(LeaveRequestCreateDto dto);
        Task<LeaveRequestDto?> UpdateAsync(long id, LeaveRequestCreateDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
