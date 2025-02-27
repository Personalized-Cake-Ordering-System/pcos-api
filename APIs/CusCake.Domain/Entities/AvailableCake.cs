using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;


[Table("available_cakes")]
public class AvailableCake : BaseEntity
{
    [Column("available_cake_price")]
    public double AvailableCakePrice { get; set; }
    [Column("available_cake_name")]
    public string AvailableCakeName { get; set; } = default!;

    [Column("available_cake_description")]
    public string? AvailableCakeDescription { get; set; }

    [Column("available_cake_type")]
    public string? AvailableCakeType { get; set; }

    [Column("available_cake_quantity")]
    public int AvailableCakeQuantity { get; set; }

    [Column("available_cake_image_id")]
    public Guid AvailableCakeImageId { get; set; }

    [Column("bakery_id")]
    public Guid BakeryId { get; set; }
    public Bakery Bakery { get; set; } = default!;
    public ICollection<OrderDetail>? OrderDetails { get; set; }

    public ICollection<CakeReview>? CakeReviews { get; set; }
}