using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IDesignationService
    {
        Task<List<DesignationDto>> GetAllAsync();
        Task<DesignationDto?> GetByIdAsync(long id);
        Task<DesignationDto> CreateAsync(DesignationDto dto);
        Task<bool> UpdateAsync(long id, DesignationDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
