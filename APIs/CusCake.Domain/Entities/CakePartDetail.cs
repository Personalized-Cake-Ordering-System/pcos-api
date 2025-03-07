using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_part_details")]
public class CakePartDetail : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    [Column("cake_part_id")]
    [JsonPropertyName("cake_part_id")]
    public Guid CakePartId { get; set; }

    [JsonPropertyName("cake_part")]
    public CakePart CakePart { get; set; } = default!;
}