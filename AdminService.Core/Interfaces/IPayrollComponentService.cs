using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IPayrollComponentService
    {
        Task<IEnumerable<PayrollComponentDto>> GetAllAsync();
        Task<PayrollComponentDto?> GetByIdAsync(long payrollComponentId);
        Task<PayrollComponentDto> CreateAsync(CreatePayrollComponentDto dto);
        Task<PayrollComponentDto?> UpdateAsync(long payrollComponentId, UpdatePayrollComponentDto dto);
        Task<bool> DeleteAsync(long payrollComponentId);
    }
}
