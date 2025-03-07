using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_extras")]
public class CakeExtra : BaseEntity
{
    [Column("extra_name")]
    [JsonPropertyName("extra_name")]
    public string ExtraName { get; set; } = default!;

    [Column("extra_price")]
    [JsonPropertyName("extra_price")]
    public double ExtraPrice { get; set; } = 0;

    [Column("extra_type")]
    [JsonPropertyName("extra_type")]
    public string ExtraType { get; set; } = default!;

    [Column("extra_color")]
    [JsonPropertyName("extra_color")]
    public string? ExtraColor { get; set; }

    [Column("extra_description")]
    [JsonPropertyName("extra_description")]
    public string? ExtraDescription { get; set; }

    [Column("is_default")]
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [Column("extra_image_id")]
    [JsonPropertyName("extra_image_id")]
    public Guid? ExtraImageId { get; set; }

    [JsonPropertyName("extra_image")]
    public Storage? ExtraImage { get; set; }

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; } = default!;

    [JsonPropertyName("cake_extra_details")]
    public ICollection<CakeExtraDetail>? CakeExtraDetails { get; set; }
}