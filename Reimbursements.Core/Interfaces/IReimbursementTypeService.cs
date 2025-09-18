using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reimbursements.Core.Interfaces
{
    public interface IReimbursementTypeService
    {
        Task<IEnumerable<ReimbursementTypeDto>> GetAllAsync();
        Task<ReimbursementTypeDto?> GetByIdAsync(long typeId);
        Task<ReimbursementTypeDto> CreateAsync(ReimbursementTypeDto dto);
        Task<ReimbursementTypeDto?> UpdateAsync(long typeId, ReimbursementTypeDto dto);
        Task<bool> DeleteAsync(long typeId);
    }
}
