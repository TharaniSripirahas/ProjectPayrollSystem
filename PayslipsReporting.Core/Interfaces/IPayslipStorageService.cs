using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipsReporting.Core.Interfaces
{
    public interface IPayslipStorageService
    {
        Task<IEnumerable<PayslipsReportingDto.PayslipDto>> GetAllAsync();
        Task<PayslipsReportingDto.PayslipDto?> GetByIdAsync(long payslipId);
        Task<PayslipsReportingDto.PayslipDto> CreateAsync(PayslipsReportingDto.CreatePayslipDto dto);
        Task<PayslipsReportingDto.PayslipDto?> UpdateAsync(long payslipId, PayslipsReportingDto.UpdatePayslipDto dto);
        Task<bool> DeleteAsync(long payslipId);
    }
}
