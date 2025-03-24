using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("cake_reviews")]
public class CakeReview : BaseEntity
{
    [Column("content")]
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [Column("rating")]
    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [Column("image_id")]
    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }
    [JsonPropertyName("image")]
    public Storage? Image { get; set; }

    [Column("order_detail_id")]
    [JsonPropertyName("order_detail_id")]
    public Guid OrderDetailId { get; set; }

    [JsonPropertyName("order_detail")]
    public OrderDetail OrderDetail { get; set; } = default!;

    [Column("available_cake_id")]
    [JsonPropertyName("available_cake_id")]
    public Guid AvailableCakeId { get; set; }

    [JsonPropertyName("available_cake")]
    public AvailableCake AvailableCake { get; set; } = default!;

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;

    [Column("customer_id")]
    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = default!;
}