using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.PayslipsReportingDto;

namespace AdminService.Core.Interfaces
{
    public interface IGeneratedReportService
    {
        Task<IEnumerable<GeneratedReportDto>> GetAllAsync();
        Task<GeneratedReportDto?> GetByIdAsync(long reportId);
        Task<GeneratedReportDto> CreateAsync(CreateGeneratedReportDto dto);
        Task<GeneratedReportDto?> UpdateAsync(long reportId, UpdateGeneratedReportDto dto);
        Task<bool> DeleteAsync(long reportId);
    }
}
