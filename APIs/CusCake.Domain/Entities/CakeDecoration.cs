using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_decorations")]
public class CakeDecoration : BaseEntity
{
    [Column("decoration_name")]
    [JsonPropertyName("decoration_name")]
    public string DecorationName { get; set; } = default!;

    [Column("decoration_price")]
    [JsonPropertyName("decoration_price")]
    public double DecorationPrice { get; set; } = 0;

    [Column("decoration_type")]
    [JsonPropertyName("decoration_type")]
    public string DecorationType { get; set; } = default!;

    [Column("decoration_description")]
    [JsonPropertyName("decoration_description")]
    public string? DecorationDescription { get; set; }

    [Column("decoration_color")]
    [JsonPropertyName("decoration_color")]
    public string? DecorationColor { get; set; }

    [Column("is_default")]
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [Column("decoration_image_id")]
    [JsonPropertyName("decoration_image_id")]
    public Guid? DecorationImageId { get; set; }

    [JsonPropertyName("decoration_image")]
    public Storage? DecorationImage { get; set; }

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; } = default!;

    [JsonPropertyName("cake_decoration_details")]
    public ICollection<CakeDecorationDetail>? CakeDecorationDetails { get; set; }
}