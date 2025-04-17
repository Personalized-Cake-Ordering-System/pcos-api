using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("available_cakes")]
public class AvailableCake : BaseEntity
{
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
}