using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IEmployeeSkillService
    {
        Task<List<EmployeeSkillDto>> GetAllAsync();
        Task<EmployeeSkillDto?> GetByIdAsync(long id);
        Task<EmployeeSkillDto> CreateAsync(EmployeeSkillCreateDto dto);  
        Task<EmployeeSkillDto?> UpdateAsync(long id, EmployeeSkillDto dto);
        Task<bool> DeleteAsync(long id);
    }

}
