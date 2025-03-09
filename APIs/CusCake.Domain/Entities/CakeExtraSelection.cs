using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_extra_selections")]
public class CakeExtraSelection : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [Column("extra_type")]
    [JsonPropertyName("extra_type")]
    public string ExtraType { get; set; } = default!;

    // [Column("extra_type_id")]
    // [JsonPropertyName("extra_type_id")]
    // public Guid ExtraTypeId { get; set; }

    [Column("extra_option_id")]
    [JsonPropertyName("extra_option_id")]
    public Guid ExtraOptionId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    // [JsonPropertyName("extra_type")]
    // public CakeExtraType ExtraType { get; set; } = default!;

    [JsonPropertyName("extra_option")]
    public CakeExtraOption ExtraOption { get; set; } = default!;
}
