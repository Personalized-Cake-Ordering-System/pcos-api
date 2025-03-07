using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

    [JsonPropertyName("sender_id")]
    [Column("sender_id")]
    public Guid SenderId { get; set; }

    [JsonPropertyName("notification_type")]
    [Column("notification_type")]
    public string NotificationType { get; set; } = default!;

    [JsonPropertyName("is_read")]
    [Column("is_read")]
    public bool IsRead { get; set; } = false;

    [JsonPropertyName("type")]
    [Column("type")]
    public string Type { get; set; } = default!;

    // Relation
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
