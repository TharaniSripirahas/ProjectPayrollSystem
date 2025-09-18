using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Core.Interfaces
{
    public interface IEmployeeDonationService
    {
        Task<IEnumerable<EmployeeDonationDto>> GetAllAsync();
        Task<EmployeeDonationDto?> GetByIdAsync(long donationId);
        Task<EmployeeDonationDto> CreateAsync(EmployeeDonationCreateDto dto);
        Task<EmployeeDonationDto?> UpdateAsync(EmployeeDonationUpdateDto dto);
        Task<bool> DeleteAsync(long donationId);
    }
}
