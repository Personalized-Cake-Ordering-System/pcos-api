using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("custom_cakes")]
public class CustomCake : BaseEntity
{
    [Column("total_price")]
    [JsonPropertyName("total_price")]
    public double Price { get; set; }

    [Column("custom_cake_name")]
    [JsonPropertyName("custom_cake_name")]
    public string CakeName { get; set; } = default!;

    [Column("custom_cake_description")]
    [JsonPropertyName("custom_cake_description")]
    public string? Description { get; set; }

    [Column("recipe")]
    [JsonPropertyName("recipe")]
    public string? Recipe { get; set; }

    [Column("customer_id")]
    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = default!;

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;

    [Column("message_selection_id")]
    [JsonPropertyName("message_selection_id")]
    public Guid MessageSelectionId { get; set; }

    [JsonPropertyName("message_selection")]
    public CakeMessageSelection MessageSelection { get; set; } = default!;

    [JsonPropertyName("part_selections")]
    public ICollection<CakePartSelection>? PartSelections { get; set; }

    [JsonPropertyName("extra_selections")]
    public ICollection<CakeExtraSelection>? ExtraSelections { get; set; }

    [JsonPropertyName("decoration_selections")]
    public ICollection<CakeDecorationSelection>? DecorationSelections { get; set; }

    [JsonPropertyName("order_details")]
    public ICollection<OrderDetail>? OrderDetails { get; set; } = default!;
}