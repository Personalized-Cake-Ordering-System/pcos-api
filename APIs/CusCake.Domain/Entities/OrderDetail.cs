using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("order_detail")]
public class OrderDetail : BaseEntity
{
    [Column("order_id")]
    public Guid OrderId { get; set; }

    public Order Order { get; set; } = default!;

    [Column("available_cake_id")]
    public Guid? AvailableCakeId { get; set; }

    public AvailableCake? AvailableCake { get; set; }

    [Column("cake_note")]
    public string? CakeNote { get; set; }

    [Column("sub_total_price")]
    public double? SubTotalPrice { get; set; }

    [Column("cake_review_id")]
    public Guid? CakeReviewId { get; set; }
    public CakeReview? CakeReview { get; set; }

    [Column("custom_cake_id")]
    public Guid? CustomCakeId { get; set; }
    public CustomCake? CustomCake { get; set; }

}