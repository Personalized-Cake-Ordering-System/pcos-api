using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;
[Table("notifications")]
public class Notification : BaseEntity
{
    [Column("title")]
    public string Title { get; set; } = default!;
    [Column("content")]
    public string Content { get; set; } = default!;
    [Column("sender_id")]
    public Guid SenderId { get; set; }
    [Column("notification_type")]
    public string NotificationType { get; set; } = default!;
    [Column("is_read")]
    public bool IsRead { get; set; } = false;
    [Column("type")]
    public string Type { get; set; } = default!;

    // Relation
    [Column("bakery_id")]
    public Guid? BakeryId { get; set; }

    public Bakery? Bakery { get; set; } = default!;

    [Column("customer_id")]
    public Guid? CustomerId { get; set; }

    public Customer? Customer { get; set; }

}