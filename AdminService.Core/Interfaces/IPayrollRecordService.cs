using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IPayrollRecordService
    {
        Task<IEnumerable<PayrollRecordDto>> GetAllAsync();
        Task<PayrollRecordDto?> GetByIdAsync(long recordId);
        Task<PayrollRecordDto> CreateAsync(CreatePayrollRecordDto dto);
        Task<PayrollRecordDto?> UpdateAsync(long recordId, UpdatePayrollRecordDto dto);
        Task<bool> DeleteAsync(long recordId);
    }
}
