using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeDto>> GetAllAsync();
        Task<LeaveTypeDto?> GetByIdAsync(long id);
        Task<LeaveTypeDto> CreateAsync(LeaveTypeDto dto);
        Task<LeaveTypeDto?> UpdateAsync(long id, LeaveTypeDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
