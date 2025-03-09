using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_extra_options")]
public class CakeExtraOption : BaseEntity
{
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [Column("price")]
    [JsonPropertyName("price")]
    public double Price { get; set; } = 0;

    [Column("color")]
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [Column("is_default")]
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [Column("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Column("image_id")]
    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("image")]
    public Storage? Image { get; set; }

    [Column("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; } = default!;


    // [Column("extra_type_id")]
    // [JsonPropertyName("extra_type_id")]
    // public Guid ExtraTypeId { get; set; }

    // [JsonPropertyName("extra_type")]
    // public CakeExtraType ExtraType { get; set; } = default!;
}