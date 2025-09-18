using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.SecurityAccessDto;

namespace AdminService.Core.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAllAsync();
        Task<AuditLogDto?> GetByIdAsync(long logId);
        Task<AuditLogDto> CreateAsync(CreateAuditLogDto dto);
        Task<AuditLogDto?> UpdateAsync(long logId, UpdateAuditLogDto dto);
        Task<bool> DeleteAsync(long logId);
    }
}
