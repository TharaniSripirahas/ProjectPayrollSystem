using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace SecurityAccess.Core.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAllAsync();
        Task<UserRoleDto?> GetByIdAsync(long roleId);
        Task<UserRoleDto> CreateAsync(CreateUserRoleDto dto);
        Task<UserRoleDto?> UpdateAsync(long roleId, UpdateUserRoleDto dto);
        Task<bool> DeleteAsync(long roleId);
    }
}
