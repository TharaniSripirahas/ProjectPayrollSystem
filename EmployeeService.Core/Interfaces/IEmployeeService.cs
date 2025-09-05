using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(long id);
       // Task<bool> CreateAsync(EmployeeDto dto);
        Task<bool> UpdateAsync(long id, EmployeeDto dto);
        Task<bool> DeleteAsync(long id);
        Task<ApiResult<EmployeeDto>> CreateEmployeeAsync(EmployeeDto dto);
    }
}
