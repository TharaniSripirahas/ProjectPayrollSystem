using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.LoansDto;

namespace AdminService.Core.Interfaces
{
    public interface IDonationFundService
    {
        Task<IEnumerable<DonationFundDto>> GetAllAsync();
        Task<DonationFundDto?> GetByIdAsync(long fundId);
        Task<DonationFundDto> CreateAsync(DonationFundCreateDto dto);
        Task<DonationFundDto?> UpdateAsync(DonationFundUpdateDto dto);
        Task<bool> DeleteAsync(long fundId);
    }
}
