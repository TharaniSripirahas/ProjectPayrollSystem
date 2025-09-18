using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Core.Interfaces
{
    public interface IPayslipNotificationService
    {
        Task<IEnumerable<PayslipNotificationDto>> GetAllAsync();
        Task<PayslipNotificationDto?> GetByIdAsync(long notificationId);
        Task<PayslipNotificationDto> CreateAsync(CreatePayslipNotificationDto dto);
        Task<PayslipNotificationDto?> UpdateAsync(long notificationId, UpdatePayslipNotificationDto dto);
        Task<bool> DeleteAsync(long notificationId);
    }
}
