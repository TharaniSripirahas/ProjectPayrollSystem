using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeductionsCompliance.Core.Interfaces
{
    public interface IStatutoryDeductionService
    {
        Task<IEnumerable<DeductionsComplianceDto.StatutoryDeductionDto>> GetAllAsync();
        Task<DeductionsComplianceDto.StatutoryDeductionDto?> GetByIdAsync(long deductionId);
        Task<DeductionsComplianceDto.StatutoryDeductionDto> CreateAsync(DeductionsComplianceDto.CreateStatutoryDeductionDto dto);
        Task<DeductionsComplianceDto.StatutoryDeductionDto?> UpdateAsync(long deductionId, DeductionsComplianceDto.UpdateStatutoryDeductionDto dto);
        Task<bool> DeleteAsync(long deductionId);
    }
}
