using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.PayslipsReportingDto;

namespace AdminService.Core.Interfaces
{
    public interface IPayslipAccessLogService
    {
        Task<IEnumerable<PayslipAccessLogDto>> GetAllAsync();
        Task<PayslipAccessLogDto?> GetByIdAsync(long logId);
        Task<PayslipAccessLogDto> CreateAsync(CreatePayslipAccessLogDto dto);
        Task<PayslipAccessLogDto?> UpdateAsync(long logId, UpdatePayslipAccessLogDto dto);
        Task<bool> DeleteAsync(long logId);
    }
}
