using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_message_details")]
public class CakeMessageDetail : BaseEntity
{
    [Column("cake_message_id")]
    [JsonPropertyName("cake_message_id")]
    public Guid CakeMessageId { get; set; }

    [JsonPropertyName("cake_message")]
    public CakeMessage CakeMessage { get; set; } = default!;

    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    [Column("message_image_id")]
    [JsonPropertyName("message_image_id")]
    public Guid? MessageImageId { get; set; } = default!;

    [JsonPropertyName("message_image_file")]
    public Storage? MessageImageFile { get; set; }

    [Column("message")]
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [Column("message_type_details")]
    [JsonPropertyName("message_type_details")]
    public List<CakeMessageTypeDetail>? MessageTypeDetails { get; set; }

    [Column("message_type")]
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; } = default!;
}

public class CakeMessageTypeDetail
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("type_name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("type_color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
}
