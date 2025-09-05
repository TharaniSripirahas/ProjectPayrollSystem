using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface ISkillService
    {
        Task<List<SkillDto>> GetAllAsync();
        Task<SkillDto?> GetByIdAsync(long id);
        Task<SkillDto> CreateAsync(SkillDto dto);
        Task<bool> UpdateAsync(long id, SkillDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
