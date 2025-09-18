using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IPerformanceMetricService
    {
        Task<List<PerformanceMetricDto>> GetAllAsync();
        Task<PerformanceMetricDto> GetByIdAsync(long id);
        Task<PerformanceMetricDto> CreateAsync(PerformanceMetricDto dto);
        Task<PerformanceMetricDto> UpdateAsync(long id, PerformanceMetricDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
