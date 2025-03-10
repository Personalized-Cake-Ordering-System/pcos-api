using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_message_selections")]
public class CakeMessageSelection : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [Column("message_type")]
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; } = default!;

    // [Column("message_type_id")]
    // [JsonPropertyName("message_type_id")]
    // public Guid MessageTypeId { get; set; }

    [Column("message_options")]
    [JsonPropertyName("message_options")]
    public ICollection<CakeMessageOption>? MessageOptions { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    // [JsonPropertyName("message_type")]
    // public CakeMessageType MessageType { get; set; } = default!;


}