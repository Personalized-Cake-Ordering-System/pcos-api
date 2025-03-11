using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_part_options")]
public class CakePartOption : BaseEntity
{
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [Column("price")]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [Column("color")]
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [Column("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Column("is_default")]
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

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

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;
    // [Column("part_type_id")]
    // [JsonPropertyName("cake_part_type_id")]
    // public Guid CakePartTypeId { get; set; }

    // [JsonPropertyName("cake_part_type")]
    // public CakePartType CakePartType { get; set; } = default!;
}