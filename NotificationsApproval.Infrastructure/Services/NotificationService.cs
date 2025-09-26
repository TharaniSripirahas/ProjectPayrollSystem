using Microsoft.EntityFrameworkCore;
using NotificationsApproval.Core.Interfaces;
using Payroll.Common.DatabaseContext;
using Payroll.Common.Models;
using Payroll.Common.NonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.Common.NonEntities.NotificationsApprovalDto;

namespace NotificationsApproval.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly PayrollDbContext _context;

        public NotificationService(PayrollDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllAsync()
        {
            var notifications = await _context.Notifications
                .Include(n => n.Recipient)
                .Include(n => n.Sender)
                .ToListAsync();

            return notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                RecipientId = n.RecipientId,
                RecipientName = n.Recipient != null ? n.Recipient.FirstName + " " + n.Recipient.LastName : null,
                SenderId = n.SenderId,
                SenderName = n.Sender != null ? n.Sender.FirstName + " " + n.Sender.LastName : null,
                Title = n.Title,
                Message = n.Message,
                NotificationType = n.NotificationType,
                ReferenceId = n.ReferenceId,
                ReferenceTable = n.ReferenceTable,
                IsRead = n.IsRead ?? 0,  
                CreatedAt = n.CreatedOn,
                ReadAt = n.ReadAt,
                DeliveryStatus = n.DeliveryStatus,
                DeliveryChannel = n.DeliveryChannel,
                RecordStatus = n.RecordStatus
            }).ToList();
        }

        public async Task<NotificationDto?> GetByIdAsync(long notificationId)
        {
            var n = await _context.Notifications
                .Include(x => x.Recipient)
                .Include(x => x.Sender)
                .FirstOrDefaultAsync(x => x.NotificationId == notificationId);

            if (n == null) return null;

            return new NotificationDto
            {
                NotificationId = n.NotificationId,
                RecipientId = n.RecipientId,
                RecipientName = n.Recipient != null ? n.Recipient.FirstName + " " + n.Recipient.LastName : null,
                SenderId = n.SenderId,
                SenderName = n.Sender != null ? n.Sender.FirstName + " " + n.Sender.LastName : null,
                Title = n.Title,
                Message = n.Message,
                NotificationType = n.NotificationType,
                ReferenceId = n.ReferenceId,
                ReferenceTable = n.ReferenceTable,
                IsRead = n.IsRead ?? 0,
                CreatedAt = n.CreatedOn,
                ReadAt = n.ReadAt,
                DeliveryStatus = n.DeliveryStatus,
                DeliveryChannel = n.DeliveryChannel,
                RecordStatus = n.RecordStatus
            };
        }

        public async Task<NotificationDto> CreateAsync(CreateNotificationDto dto)
        {
            var recipient = await _context.Employees.FindAsync(dto.RecipientId);
            var sender = await _context.Employees.FindAsync(dto.SenderId);

            var entity = new Notification
            {
                RecipientId = dto.RecipientId,
                SenderId = dto.SenderId,
                Title = dto.Title,
                Message = dto.Message,
                NotificationType = dto.NotificationType,
                ReferenceId = dto.ReferenceId,
                ReferenceTable = dto.ReferenceTable,
                DeliveryStatus = dto.DeliveryStatus,
                DeliveryChannel = dto.DeliveryChannel,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                RecordStatus = 1,
                IsRead = 0, 
                ReadAt = null,
                Recipient = recipient,
                Sender = sender
            };

            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync();

            return new NotificationDto
            {
                NotificationId = entity.NotificationId,
                RecipientId = entity.RecipientId,
                RecipientName = recipient != null ? recipient.FirstName + " " + recipient.LastName : null,
                SenderId = entity.SenderId,
                SenderName = sender != null ? sender.FirstName + " " + sender.LastName : null,
                Title = entity.Title,
                Message = entity.Message,
                NotificationType = entity.NotificationType,
                ReferenceId = entity.ReferenceId,
                ReferenceTable = entity.ReferenceTable,
                IsRead = entity.IsRead,
                CreatedAt = entity.CreatedOn,
                ReadAt = entity.ReadAt,
                DeliveryStatus = entity.DeliveryStatus,
                DeliveryChannel = entity.DeliveryChannel,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<NotificationDto?> UpdateAsync(long notificationId, UpdateNotificationDto dto)
        {
            var entity = await _context.Notifications.FindAsync(notificationId);
            if (entity == null) return null;

            entity.RecipientId = dto.RecipientId;
            entity.SenderId = dto.SenderId;
            entity.Title = dto.Title;
            entity.Message = dto.Message;
            entity.NotificationType = dto.NotificationType;
            entity.ReferenceId = dto.ReferenceId;
            entity.ReferenceTable = dto.ReferenceTable;

            if ((entity.IsRead ?? 0) == 0 && dto.IsRead == 1)
            {
                entity.ReadAt = DateTime.UtcNow;
            }
            entity.IsRead = dto.IsRead;

            entity.DeliveryStatus = dto.DeliveryStatus;
            entity.DeliveryChannel = dto.DeliveryChannel;
            entity.RecordStatus = dto.RecordStatus;
            entity.LastModifiedBy = 1; 
            entity.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new NotificationDto
            {
                NotificationId = entity.NotificationId,
                RecipientId = entity.RecipientId,
                RecipientName = entity.Recipient != null ? entity.Recipient.FirstName + " " + entity.Recipient.LastName : null,
                SenderId = entity.SenderId,
                SenderName = entity.Sender != null ? entity.Sender.FirstName + " " + entity.Sender.LastName : null,
                Title = entity.Title,
                Message = entity.Message,
                NotificationType = entity.NotificationType,
                ReferenceId = entity.ReferenceId,
                ReferenceTable = entity.ReferenceTable,
                IsRead = entity.IsRead,
                CreatedAt = entity.CreatedOn,
                ReadAt = entity.ReadAt,
                DeliveryStatus = entity.DeliveryStatus,
                DeliveryChannel = entity.DeliveryChannel,
                RecordStatus = entity.RecordStatus
            };
        }

        public async Task<bool> DeleteAsync(long notificationId)
        {
            var entity = await _context.Notifications.FindAsync(notificationId);
            if (entity == null) return false;

            _context.Notifications.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
