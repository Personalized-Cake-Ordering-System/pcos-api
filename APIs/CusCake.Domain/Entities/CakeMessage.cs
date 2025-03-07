using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_messages")]
public class CakeMessage : BaseEntity
{
    [Column("message_name")]
    [JsonPropertyName("message_name")]
    public string MessageName { get; set; } = default!;

    [Column("message_image_id")]
    [JsonPropertyName("message_image_id")]
    public Guid? MessageImageId { get; set; } = default!;

    [JsonPropertyName("message_image")]
    public Storage? MessageImage { get; set; }

    [Column("message_price")]
    [JsonPropertyName("message_price")]
    public double MessagePrice { get; set; } = 0;

    [Column("message_type")]
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; } = default!;

    [Column("message_description")]
    [JsonPropertyName("message_description")]
    public string? MessageDescription { get; set; }

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; } = default!;

    [JsonPropertyName("cake_message_types")]
    public ICollection<CakeMessageType>? CakeMessageTypes { get; set; }

    [JsonPropertyName("cake_message_details")]
    public ICollection<CakeMessageDetail>? CakeMessageDetails { get; set; }
}

[Table("cake_message_types")]
public class CakeMessageType : BaseEntity
{
    [Column("message_type")]
    [JsonPropertyName("message_type")]
    public string Type { get; set; } = default!;

    [Column("message_name")]
    [JsonPropertyName("message_name")]
    public string Name { get; set; } = default!;

    [Column("message_color")]
    [JsonPropertyName("message_color")]
    public string? Color { get; set; } = default!;

    [Column("cake_message_id")]
    [JsonPropertyName("cake_message_id")]
    public Guid CakeMessageId { get; set; }

    [JsonPropertyName("cake_message")]
    public CakeMessage CakeMessage { get; set; } = default!;
}