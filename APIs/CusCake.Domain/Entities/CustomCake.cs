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

    [JsonPropertyName("cake_message_details")]
    public ICollection<CakeMessageDetail>? CakeMessageDetails { get; set; }

    [JsonPropertyName("cake_part_details")]
    public ICollection<CakePartDetail>? CakePartDetails { get; set; }

    [JsonPropertyName("cake_extra_details")]
    public ICollection<CakeExtraDetail>? CakeExtraDetails { get; set; }

    [JsonPropertyName("cake_decoration_details")]
    public ICollection<CakeDecorationDetail>? CakeDecorationDetails { get; set; }

    [JsonPropertyName("order_details")]
    public ICollection<OrderDetail>? OrderDetails { get; set; } = default!;
}