using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.NonEntities
{
    public class NotificationsApprovalDto
    {
        public class NotificationDto
        {
            public long NotificationId { get; set; }
            public long RecipientId { get; set; }
            public string? RecipientName { get; set; }  
            public long SenderId { get; set; }
            public string? SenderName { get; set; }     
            public string Title { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public string NotificationType { get; set; } = string.Empty;
            public long ReferenceId { get; set; }
            public string ReferenceTable { get; set; } = string.Empty;
            public int? IsRead { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? ReadAt { get; set; }
            public int DeliveryStatus { get; set; }
            public string? DeliveryChannel { get; set; }
            public int RecordStatus { get; set; }
        }


        public class CreateNotificationDto
        {
            public long NotificationId { get; set; }
            public long RecipientId { get; set; }
            public long SenderId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public string NotificationType { get; set; } = string.Empty;
            public long ReferenceId { get; set; }
            public string ReferenceTable { get; set; } = string.Empty;
            public int DeliveryStatus { get; set; }
            public string? DeliveryChannel { get; set; }
            public int RecordStatus { get; set; }

        }

        public class UpdateNotificationDto : CreateNotificationDto
        {
            public int? IsRead { get; set; }
            public int RecordStatus { get; set; }
        }

        public class ApprovalWorkflowDto
        {
            public long WorkflowId { get; set; }
            public string WorkflowName { get; set; } = string.Empty;
            public string EntityType { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public DateTime? CreatedAt { get; set; }
        }

        public class CreateApprovalWorkflowDto
        {
            public long WorkflowId { get; set; }           
            public string WorkflowName { get; set; } = string.Empty;
            public string EntityType { get; set; } = string.Empty;
            public int IsActive { get; set; } = 1;
            public DateTime? CreatedAt { get; set; }       
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;
        }


        public class UpdateApprovalWorkflowDto
        {
            public string WorkflowName { get; set; } = string.Empty;
            public string EntityType { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public long? LastModifiedBy { get; set; }
            public int RecordStatus { get; set; } = 0;

        }
        public class ApprovalLevelDto
        {
            public long LevelId { get; set; }
            public long WorkflowId { get; set; }
            public string? WorkflowName { get; set; }  
            public int LevelNumber { get; set; }
            public long ApproverRoleId { get; set; }
            public string? ApproverRoleName { get; set; } 
            public bool IsFinalApproval { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;
        }


        public class CreateApprovalLevelDto
        {
            public long LevelId { get; set; }
            public long WorkflowId { get; set; }
            public int LevelNumber { get; set; }
            public long ApproverRoleId { get; set; }
            public bool IsFinalApproval { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;
        }


        public class UpdateApprovalLevelDto
        {
            public int LevelNumber { get; set; }
            public long ApproverRoleId { get; set; }
            public bool IsFinalApproval { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;
        }

        //public class ApprovalRequestDto
        //{
        //    public long RequestId { get; set; }
        //    public long WorkflowId { get; set; }
        //    public string? WorkflowName { get; set; } 
        //    public long CurrentLevelId { get; set; }
        //    public string? ApproverName { get; set; }
        //    public string? LevelName { get; set; }
        //    public string? CurrentLevelName { get; set; }
        //    public string? RequesterName { get; set; }
        //    public string? EntityTable { get; set; }
        //    public long? RequesterId { get; set; }
        //    public DateTime? CreatedAt { get; set; }
        //    public DateTime? UpdatedAt { get; set; }
        //    public long CreatedBy { get; set; }
        //    public DateTime CreatedOn { get; set; }
        //    public long? LastModifiedBy { get; set; }
        //    public DateTime? LastModifiedOn { get; set; }
        //    public int RecordStatus { get; set; } = 0;
        //}
        public class ApprovalRequestDto
        {
            public long RequestId { get; set; }
            public long WorkflowId { get; set; }
            public string? WorkflowName { get; set; }
            public long CurrentLevelId { get; set; }
            //public string? ApproverName { get; set; }
            //public string? LevelName { get; set; }
            public string? CurrentLevelName { get; set; }
            public string? RequesterName { get; set; }
            public string? EntityTable { get; set; }
            public long? RequesterId { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public long CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;
        }

        public class CreateApprovalRequestDto
        {
            public long RequestId { get; set; }
            public long WorkflowId { get; set; }
            public long CurrentLevelId { get; set; }
            public string? EntityTable { get; set; }
            public long? RequesterId { get; set; }

            public long CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }

            public int RecordStatus { get; set; } = 0;
        }


        public class UpdateApprovalRequestDto
        {
            public long CurrentLevelId { get; set; }
            public string? EntityTable { get; set; }
            public long? RequesterId { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public int RecordStatus { get; set; } = 0;
        }
        public class ApprovalActionDto
        {
            public long ActionId { get; set; }
            public long RequestId { get; set; }
            public string? RequestName { get; set; }
            public long LevelId { get; set; }
            public long ApproverId { get; set; }
            public string? ApproverName { get; set; }
            public string ActionType { get; set; } = string.Empty;
            public string? Comments { get; set; }
            public DateTime ActionDate { get; set; }

        }

        public class CreateApprovalActionDto
        {
            public long RequestId { get; set; }
            public long LevelId { get; set; }
            public long ApproverId { get; set; }
            public string ActionType { get; set; } = string.Empty;
            public string? Comments { get; set; }
            public long CreatedBy { get; set; }
        }

        public class UpdateApprovalActionDto
        {
            public string ActionType { get; set; } = string.Empty;
            public string? Comments { get; set; }
            public long? LastModifiedBy { get; set; }
        }
        //public class ApprovalActionDto
        //{
        //    public long ActionId { get; set; }
        //    public long RequestId { get; set; }
        //    public string? RequesterName { get; set; }
        //    public long LevelId { get; set; }
        //    public string? LevelName { get; set; }
        //    public long ApproverId { get; set; }
        //    public string? ApproverName { get; set; }
        //    public string ActionType { get; set; } = string.Empty;
        //    public string? Comments { get; set; }
        //    public DateTime ActionDate { get; set; }
        //    public int RecordStatus { get; set; } = 0;
        //}

        //public class CreateApprovalActionDto
        //{
        //    public long RequestId { get; set; }
        //    public long LevelId { get; set; }
        //    public long ApproverId { get; set; }
        //    public string ActionType { get; set; } = null!;
        //    public string? Comments { get; set; }
        //    public long CreatedBy { get; set; }
        //    public int RecordStatus { get; set; }
        //}

        //public class UpdateApprovalActionDto
        //{
        //    public string ActionType { get; set; } = string.Empty;
        //    public string? Comments { get; set; }
        //    public long? LastModifiedBy { get; set; }
        //}
    }
}

