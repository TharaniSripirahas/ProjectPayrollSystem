using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IProjectPerformanceService
    {
        Task<List<ProjectPerformanceDto>> GetAllAsync();
        Task<ProjectPerformanceDto> GetByIdAsync(long id);
        Task<ProjectPerformanceDto> CreateAsync(ProjectPerformanceDto dto);
        Task<ProjectPerformanceDto> UpdateAsync(long id, ProjectPerformanceDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
