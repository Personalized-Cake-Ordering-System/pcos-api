using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

namespace CusCake.Domain.Entities;

[Table("notifications")]
public class Notification : BaseEntity
{
    [JsonPropertyName("title")]
    [Column("title")]
    public string Title { get; set; } = default!;

    [JsonPropertyName("content")]
    [Column("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("sender_type")]
    [Column("sender_type")]
    public string SenderType { get; set; } = NotificationSenderType.SYSTEM;

    [JsonPropertyName("type")]
    [Column("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("is_read")]
    [Column("is_read")]
    public bool IsRead { get; set; } = false;

    [JsonPropertyName("target_entity_id")]
    [Column("target_entity_id")]
    public Guid TargetEntityId { get; set; } = default!;

    // Relation
    [JsonPropertyName("admin_id")]
    [Column("admin_id")]
    public Guid? AdminId { get; set; }

    [JsonPropertyName("admin")]
    public Admin? Admin { get; set; } = default!;


    [JsonPropertyName("bakery_id")]
    [Column("bakery_id")]
    public Guid? BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery? Bakery { get; set; } = default!;

    [JsonPropertyName("customer_id")]
    [Column("customer_id")]
    public Guid? CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }
}
