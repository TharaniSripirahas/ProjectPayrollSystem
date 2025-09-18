using AdminService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;

namespace AdminService.Infrastructure.Services
{
    public class PayslipNotificationService : IPayslipNotificationService
    {
        private readonly PayrollDbContext _context;

        public PayslipNotificationService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayslipNotificationDto>> GetAllAsync()
        {
            return await _context.PayslipNotifications
                .Include(n => n.Employee)
                .Include(n => n.PayrollCycle)
                .Select(n => new PayslipNotificationDto
                {
                    NotificationId = n.NotificationId,
                    EmployeeId = n.EmployeeId,
                    PayrollCycleId = n.PayrollCycleId,
                    NotificationMethod = n.NotificationMethod,
                    Status = n.Status,
                    RecordStatus = n.RecordStatus,
                    PayrollCycleName = n.PayrollCycle != null ? n.PayrollCycle.PayrollCycleName : null,
                    EmployeeName = n.Employee != null ? n.Employee.FirstName + " " + n.Employee.LastName : null
                })
                .ToListAsync();
        }

        public async Task<PayslipNotificationDto?> GetByIdAsync(long notificationId)
        {
            var notification = await _context.PayslipNotifications
                .Include(n => n.Employee)
                .Include(n => n.PayrollCycle)
                .FirstOrDefaultAsync(n => n.NotificationId == notificationId);

            if (notification == null) return null;

            return new PayslipNotificationDto
            {
                NotificationId = notification.NotificationId,
                EmployeeId = notification.EmployeeId,
                PayrollCycleId = notification.PayrollCycleId,
                NotificationMethod = notification.NotificationMethod,
                Status = notification.Status,
                RecordStatus = notification.RecordStatus,
                PayrollCycleName = notification.PayrollCycle != null ? notification.PayrollCycle.PayrollCycleName : null,
                EmployeeName = notification.Employee != null ? notification.Employee.FirstName + " " + notification.Employee.LastName : null
            };
        }

        public async Task<PayslipNotificationDto> CreateAsync(CreatePayslipNotificationDto dto)
        {
            var notification = new PayslipNotification
            {
                EmployeeId = dto.EmployeeId,
                PayrollCycleId = dto.PayrollCycleId,
                NotificationMethod = dto.NotificationMethod,
                Status = dto.Status,
                CreatedBy = 1, // replace with logged-in user
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1
            };

            _context.PayslipNotifications.Add(notification);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(notification.NotificationId) ?? throw new Exception("Failed to create notification.");
        }

        public async Task<PayslipNotificationDto?> UpdateAsync(long notificationId, UpdatePayslipNotificationDto dto)
        {
            var notification = await _context.PayslipNotifications.FindAsync(notificationId);
            if (notification == null) return null;

            notification.EmployeeId = dto.EmployeeId;
            notification.PayrollCycleId = dto.PayrollCycleId;
            notification.NotificationMethod = dto.NotificationMethod;
            notification.Status = dto.Status;
            notification.LastModifiedBy = 1; // replace with logged-in user
            notification.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(notification.NotificationId);
        }

        public async Task<bool> DeleteAsync(long notificationId)
        {
            var notification = await _context.PayslipNotifications.FindAsync(notificationId);
            if (notification == null) return false;

            _context.PayslipNotifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
