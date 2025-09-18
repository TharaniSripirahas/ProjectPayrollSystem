using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsApproval.Core.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationsApprovalDto.NotificationDto>> GetAllAsync();
        Task<NotificationsApprovalDto.NotificationDto?> GetByIdAsync(long notificationId);
        Task<NotificationsApprovalDto.NotificationDto> CreateAsync(NotificationsApprovalDto.CreateNotificationDto dto);
        Task<NotificationsApprovalDto.NotificationDto?> UpdateAsync(long notificationId, NotificationsApprovalDto.UpdateNotificationDto dto);
        Task<bool> DeleteAsync(long notificationId);
    }
}
