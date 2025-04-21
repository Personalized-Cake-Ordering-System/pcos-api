using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("available_cakes")]
public class AvailableCake : BaseEntity
{
    [Column("available_cake_size")]
    [JsonPropertyName("available_cake_size")]
    public string? AvailableCakeSize { get; set; } // VD: "15cm"

    [Column("available_cake_serving_size")]
    [JsonPropertyName("available_cake_serving_size")]
    public string? AvailableCakeServingSize { get; set; } // VD: "8-10 người"

    [Column("has_low_shipping_fee")]
    [JsonPropertyName("has_low_shipping_fee")]
    public bool HasLowShippingFee { get; set; } = false;

    [Column("is_quality_guaranteed")]
    [JsonPropertyName("is_quality_guaranteed")]
    public bool IsQualityGuaranteed { get; set; } = false;

    [Column("available_cake_price")]
    [JsonPropertyName("available_cake_price")]
    public double AvailableCakePrice { get; set; }

    [Column("available_cake_name")]
    [JsonPropertyName("available_cake_name")]
    public string AvailableCakeName { get; set; } = default!;

    [Column("available_cake_description")]
    [JsonPropertyName("available_cake_description")]
    public string? AvailableCakeDescription { get; set; }

    [Column("available_cake_type")]
    [JsonPropertyName("available_cake_type")]
    public string? AvailableCakeType { get; set; }

    [Column("available_cake_quantity")]
    [JsonPropertyName("available_cake_quantity")]
    public int AvailableCakeQuantity { get; set; }

    [Column("quantity_default")]
    [JsonPropertyName("quantity_default")]
    public int QuantityDefault { get; set; }

    [Column("available_main_image_id")]
    [JsonPropertyName("available_main_image_id")]
    public Guid? AvailableCakeMainImageId { get; set; }

    [JsonPropertyName("available_cake_main_image")]
    public Storage? AvailableCakeMainImage { get; set; }

    [Column("available_cake_image_files")]
    [JsonPropertyName("available_cake_image_files")]
    public List<Storage> AvailableCakeImageFiles { get; set; } = [];

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;

    // [JsonPropertyName("order_details")]
    // public ICollection<OrderDetail>? OrderDetails { get; set; }

    [JsonPropertyName("cake_reviews")]
    public ICollection<Review>? Reviews { get; set; }

    [JsonPropertyName("metric")]
    public AvailableCakeMetric? Metric { get; set; }
}