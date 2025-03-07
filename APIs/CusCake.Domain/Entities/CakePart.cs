using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_parts")]
public class CakePart : BaseEntity
{
    [Column("part_name")]
    [JsonPropertyName("part_name")]
    public string PartName { get; set; } = default!;

    [Column("part_price")]
    [JsonPropertyName("part_price")]
    public double PartPrice { get; set; } = 0;

    [Column("part_size")]
    [JsonPropertyName("part_size")]
    public double? PartSize { get; set; }

    [Column("part_type")]
    [JsonPropertyName("part_type")]
    public string PartType { get; set; } = default!;

    [Column("part_color")]
    [JsonPropertyName("part_color")]
    public string? PartColor { get; set; }

    [Column("part_description")]
    [JsonPropertyName("part_description")]
    public string? PartDescription { get; set; }

    [Column("is_default")]
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [Column("part_image_id")]
    [JsonPropertyName("part_image_id")]
    public Guid? PartImageId { get; set; }

    [JsonPropertyName("part_image")]
    public Storage? PartImage { get; set; }

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; } = default!;

    [JsonPropertyName("cake_part_details")]
    public ICollection<CakePartDetail>? CakePartDetails { get; set; }
}