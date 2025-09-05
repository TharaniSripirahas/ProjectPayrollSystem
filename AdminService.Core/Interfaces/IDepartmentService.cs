using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(long id);
        Task<DepartmentDto> CreateAsync(DepartmentDto dto);
        Task<bool> UpdateAsync(long id, DepartmentDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
