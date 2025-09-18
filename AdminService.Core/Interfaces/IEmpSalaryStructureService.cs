using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IEmpSalaryStructureService
    {
        Task<IEnumerable<EmpSalaryStructureDto>> GetAllAsync();
        Task<EmpSalaryStructureDto?> GetByIdAsync(long id);
        Task<EmpSalaryStructureDto> CreateAsync(EmpSalaryStructureDto dto);
        Task<EmpSalaryStructureDto?> UpdateAsync(long id, EmpSalaryStructureDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
