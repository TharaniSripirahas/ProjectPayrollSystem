using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IEmployeeProjectMappingService
    {
        Task<List<EmployeeProjectMappingDto>> GetAllAsync();
        Task<EmployeeProjectMappingDto> GetByIdAsync(long id);
        Task<EmployeeProjectMappingDto> CreateAsync(EmployeeProjectMappingDto dto);
        Task<EmployeeProjectMappingDto> UpdateAsync(long id, EmployeeProjectMappingDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
