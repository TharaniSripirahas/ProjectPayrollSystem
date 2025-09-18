using System;
using System.Collections.Generic;

namespace Payroll.Common.Models;

public partial class Notification
{
    public long NotificationId { get; set; }

    public long RecipientId { get; set; }

    public long SenderId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string NotificationType { get; set; } = null!;

    public long ReferenceId { get; set; }

    public string ReferenceTable { get; set; } = null!;

    public int? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public int DeliveryStatus { get; set; }

    public string? DeliveryChannel { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int RecordStatus { get; set; }

    public virtual Employee Recipient { get; set; } = null!;

    public virtual Employee Sender { get; set; } = null!;
}
