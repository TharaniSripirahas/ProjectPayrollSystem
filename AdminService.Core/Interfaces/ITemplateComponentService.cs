using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface ITemplateComponentService
    {
        Task<IEnumerable<TemplateComponentDto>> GetAllAsync();
        Task<TemplateComponentDto?> GetByIdAsync(long id);
        Task<TemplateComponentDto> CreateAsync(TemplateComponentDto dto);
        Task<TemplateComponentDto?> UpdateAsync(long id, TemplateComponentDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
