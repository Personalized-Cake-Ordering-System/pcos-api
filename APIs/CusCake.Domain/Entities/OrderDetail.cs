using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("order_detail")]
public class OrderDetail : BaseEntity
{
    [JsonPropertyName("order_id")]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [JsonPropertyName("order")]
    public Order Order { get; set; } = default!;

    [JsonPropertyName("available_cake_id")]
    [Column("available_cake_id")]
    public Guid? AvailableCakeId { get; set; }

    [JsonPropertyName("available_cake")]
    public AvailableCake? AvailableCake { get; set; }

    [JsonPropertyName("cake_note")]
    [Column("cake_note")]
    public string? CakeNote { get; set; }

    [JsonPropertyName("sub_total_price")]
    [Column("sub_total_price")]
    public double? SubTotalPrice { get; set; }

    [JsonPropertyName("quantity")]
    [Column("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("review_id")]
    [Column("review_id")]
    public Guid? CakeReviewId { get; set; }

    [JsonPropertyName("review")]
    public Review? Review { get; set; }

    [JsonPropertyName("custom_cake_id")]
    [Column("custom_cake_id")]
    public Guid? CustomCakeId { get; set; }

    [JsonPropertyName("custom_cake")]
    public CustomCake? CustomCake { get; set; }
}
