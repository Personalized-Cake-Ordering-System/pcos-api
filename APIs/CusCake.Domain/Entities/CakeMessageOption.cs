using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_message_options")]
public class CakeMessageOption : BaseEntity
{
    [Column("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [Column("color")]
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; } = default!;

    // [Column("message_type_id")]
    // [JsonPropertyName("message_type_id")]
    // public Guid MessageTypeId { get; set; }

    // [JsonPropertyName("message_type")]
    // public CakeMessageType MessageType { get; set; } = default!;
}

