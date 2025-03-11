using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_message_selections")]
public class CakeMessageSelection : BaseEntity
{
    [Column("text")]
    [JsonPropertyName("text")]
    public string? Text { get; set; } = default!;

    [Column("message_type")]
    [JsonPropertyName("message")]
    public string MessageType { get; set; } = default!;

    [Column("image_id")]
    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("image")]
    public Storage? Image { get; set; }

    [Column("message_options")]
    [JsonPropertyName("message_options")]
    public List<CakeMessageOption>? MessageOptions { get; set; }

    // [Column("message_type_id")]
    // [JsonPropertyName("message_type_id")]
    // public Guid MessageTypeId { get; set; }

    // [JsonPropertyName("message_type")]
    // public CakeMessageType MessageType { get; set; } = default!;


}