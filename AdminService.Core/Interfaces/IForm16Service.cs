using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IForm16Service
    {
        Task<IEnumerable<DeductionsComplianceDto.Form16Dto>> GetAllAsync();
        Task<DeductionsComplianceDto.Form16Dto?> GetByIdAsync(long form16Id);
        Task<DeductionsComplianceDto.Form16Dto> CreateAsync(DeductionsComplianceDto.CreateForm16Dto dto);
        Task<DeductionsComplianceDto.Form16Dto?> UpdateAsync(long form16Id, DeductionsComplianceDto.UpdateForm16Dto dto);
        Task<bool> DeleteAsync(long form16Id);
    }
}
