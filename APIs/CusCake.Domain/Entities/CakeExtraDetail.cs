using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_extra_details")]
public class CakeExtraDetail : BaseEntity
{
    [Column("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid CustomCakeId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake CustomCake { get; set; } = default!;

    [Column("cake_extra_id")]
    [JsonPropertyName("cake_extra_id")]
    public Guid CakeExtraId { get; set; }

    [JsonPropertyName("cake_extra")]
    public CakeExtra CakeExtra { get; set; } = default!;
}
