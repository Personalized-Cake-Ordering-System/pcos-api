using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_decoration_options")]
public class CakeDecorationOption : BaseEntity
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

    // [Column("decoration_type_id")]
    // [JsonPropertyName("decoration_type_id")]
    // public Guid DecorationTypeId { get; set; }

    // [JsonPropertyName("decoration_type")]
    // public CakeDecorationType DecorationType { get; set; } = default!;
}