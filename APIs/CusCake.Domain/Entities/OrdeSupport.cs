using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("order_supports")]
public class OrderSupport : BaseEntity
{
    [Column("content")]
    public string? Content { get; set; }

    [Column("order_support_file_id")]
    public Guid? OrderSupportFileId { get; set; }

    [Column("bakery_id")]
    public Guid BakeryId { get; set; }
    public Bakery Bakery { get; set; } = default!;

    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    [Column("order_id")]
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
}