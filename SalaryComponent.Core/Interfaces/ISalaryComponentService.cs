using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryComponent.Core.Interfaces
{
    public interface ISalaryComponentService
    {
        Task<IEnumerable<SalaryComponentDto>> GetAllAsync();
        Task<SalaryComponentDto?> GetByIdAsync(long id);
        Task<SalaryComponentDto> CreateAsync(SalaryComponentDto dto);
        Task<SalaryComponentDto?> UpdateAsync(long id, SalaryComponentDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
