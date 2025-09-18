using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface ITaxDeclarationService
    {
        Task<IEnumerable<DeductionsComplianceDto.TaxDeclarationDto>> GetAllAsync();
        Task<DeductionsComplianceDto.TaxDeclarationDto?> GetByIdAsync(long declarationId);
        Task<DeductionsComplianceDto.TaxDeclarationDto> CreateAsync(DeductionsComplianceDto.CreateTaxDeclarationDto dto);
        Task<DeductionsComplianceDto.TaxDeclarationDto?> UpdateAsync(long declarationId, DeductionsComplianceDto.UpdateTaxDeclarationDto dto);
        Task<bool> DeleteAsync(long declarationId);
    }
}
