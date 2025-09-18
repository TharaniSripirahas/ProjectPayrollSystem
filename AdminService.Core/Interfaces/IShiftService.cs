using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IShiftService
    {
        Task<List<ShiftDto>> GetAllAsync();
        Task<ShiftDto?> GetByIdAsync(long id);
        Task<ShiftDto> CreateAsync(ShiftDto dto);
        Task<ShiftDto?> UpdateAsync(long id, ShiftDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
