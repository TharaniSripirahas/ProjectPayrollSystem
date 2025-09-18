using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBasedVariable.Core.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(long projectId);
        Task<ProjectDto> CreateAsync(ProjectDto dto);
        Task<ProjectDto?> UpdateAsync(long projectId, ProjectDto dto);
        Task<bool> DeleteAsync(long projectId);
    }
}
