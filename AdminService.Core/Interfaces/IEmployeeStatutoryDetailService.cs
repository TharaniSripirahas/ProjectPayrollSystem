using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IEmployeeStatutoryDetailService
    {
        Task<IEnumerable<DeductionsComplianceDto.EmployeeStatutoryDetailDto>> GetAllAsync();
        Task<DeductionsComplianceDto.EmployeeStatutoryDetailDto?> GetByIdAsync(long detailsId);
        Task<DeductionsComplianceDto.EmployeeStatutoryDetailDto> CreateAsync(DeductionsComplianceDto.CreateEmployeeStatutoryDetailDto dto);
        Task<DeductionsComplianceDto.EmployeeStatutoryDetailDto?> UpdateAsync(long detailsId, DeductionsComplianceDto.UpdateEmployeeStatutoryDetailDto dto);
        Task<bool> DeleteAsync(long detailsId);
    }
}
