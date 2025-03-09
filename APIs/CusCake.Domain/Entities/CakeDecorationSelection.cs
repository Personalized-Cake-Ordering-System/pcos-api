using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_decoration_selections")]
public class CakeDecorationSelection : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [Column("decoration_type")]
    [JsonPropertyName("decoration_type")]
    public string DecorationType { get; set; } = default!;

    // [Column("decoration_type_id")]
    // [JsonPropertyName("decoration_type_id")]
    // public Guid DecorationTypeId { get; set; }

    [Column("decoration_option_id")]
    [JsonPropertyName("decoration_option_id")]
    public Guid DecorationOptionId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;


    // [JsonPropertyName("decoration_type")]
    // public CakeDecorationType DecorationType { get; set; } = default!;

    [JsonPropertyName("decoration_option")]
    public CakeDecorationOption DecorationOption { get; set; } = default!;
}