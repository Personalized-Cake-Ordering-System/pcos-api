using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_part_selections")]
public class CakePartSelection : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [Column("part_type")]
    [JsonPropertyName("part_type")]
    public string PartType { get; set; } = default!;

    // [Column("part_type_id")]
    // [JsonPropertyName("part_type_id")]
    // public Guid PartTypeId { get; set; }

    [Column("part_option_id")]
    [JsonPropertyName("part_option_id")]
    public Guid PartOptionId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    // [JsonPropertyName("part_type")]
    // public CakePartType PartType { get; set; } = default!;

    [JsonPropertyName("part_option")]
    public CakePartOption PartOption { get; set; } = default!;
}