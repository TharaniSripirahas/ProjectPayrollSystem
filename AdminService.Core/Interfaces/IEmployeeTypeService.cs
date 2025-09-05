using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IEmployeeTypeService
    {
        Task<List<EmployeeTypeDto>> GetAllAsync();
        Task<EmployeeTypeDto?> GetByIdAsync(long id);
        Task<EmployeeTypeDto> CreateAsync(EmployeeTypeDto req);
        Task<bool> UpdateAsync(long id, EmployeeTypeDto req);
        Task<bool> DeleteAsync(long id);
    }
}
