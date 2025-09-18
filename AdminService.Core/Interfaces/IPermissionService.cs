using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace AdminService.Core.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllAsync();
        Task<PermissionDto?> GetByIdAsync(long permissionId);
        Task<PermissionDto> CreateAsync(CreatePermissionDto dto);
        Task<PermissionDto?> UpdateAsync(long permissionId, UpdatePermissionDto dto);
        Task<bool> DeleteAsync(long permissionId);
    }
}
