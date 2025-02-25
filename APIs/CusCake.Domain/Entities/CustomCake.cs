using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;


[Table("custom_cakes")]
public class CustomCake : BaseEntity
{
    [Column("total_price")]
    public double Price { get; set; }
    [Column("custom_cake_name")]
    public string CakeName { get; set; } = default!;

    [Column("custom_cake_description")]
    public string? Description { get; set; }

    [Column("recipe")]
    public string? Recipe { get; set; }

    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    [Column("bakery_id")]
    public Guid BakeryId { get; set; }
    public Bakery Bakery { get; set; } = default!;

    public ICollection<CakeMessage>? CakeMessages { get; set; }

    public ICollection<CakePartDetail>? CakePartDetails { get; set; }
    public ICollection<CakeExtraDetail>? CakeExtraDetails { get; set; }
    public ICollection<CakeDecorationDetail>? CakeDecorationDetails { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; } = default!;


}