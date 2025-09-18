using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IPayrollCycleService
    {
        Task<IEnumerable<PayrollCycleDto>> GetAllAsync();
        Task<PayrollCycleDto?> GetByIdAsync(long payrollCycleId);
        Task<PayrollCycleDto> CreateAsync(CreatePayrollCycleDto dto);
        Task<PayrollCycleDto?> UpdateAsync(long payrollCycleId, UpdatePayrollCycleDto dto);
        Task<bool> DeleteAsync(long payrollCycleId);
    }
}
