using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_reviews")]
public class CakeReview : BaseEntity
{

    [Column("content")]
    public string? Content { get; set; }
    [Column("rating")]
    public int Rating { get; set; }
    [Column("review_image_file_id")]
    public Guid ReviewImageFileId { get; set; }

    [Column("order_detail_id")]
    public Guid OrderDetailId { get; set; }
    public OrderDetail OrderDetail { get; set; } = default!;

    [Column("available_cake_id")]
    public Guid AvailableCakeId { get; set; }
    public AvailableCake AvailableCake { get; set; } = default!;
    [Column("bakery_id")]
    public Guid BakeryId { get; set; }
    public Bakery Bakery { get; set; } = default!;
    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

}