using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_decoration_details")]
public class CakeDecorationDetail : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    [Column("cake_extra_id")]
    [JsonPropertyName("cake_decoration_id")]
    public Guid CakeDecorationId { get; set; }

    [JsonPropertyName("cake_decoration")]
    public CakeDecoration CakeDecoration { get; set; } = default!;
}